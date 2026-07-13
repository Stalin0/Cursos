using LedgerService.Consumers;
using LedgerService.Data;
using LedgerService.Repositories;
using LedgerService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";
var ledgerDatabaseConnectionString = builder.Configuration.GetConnectionString("LedgerDatabase")
    ?? throw new InvalidOperationException("LedgerDatabase connection string is required.");

builder.Services.AddDbContext<LedgerDbContext>(options =>
    options.UseNpgsql(ledgerDatabaseConnectionString));

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OpenAccountConsumer>();
    configurator.AddConsumer<GetAccountByIdConsumer>();
    configurator.AddConsumer<DepositFundsConsumer>();
    configurator.AddConsumer<TransferFundsConsumer>();

    configurator.UsingRabbitMq((context, rabbitMq) =>
    {
        rabbitMq.Host(rabbitMqHost, rabbitMqVirtualHost, host =>
        {
            host.Username(rabbitMqUsername);
            host.Password(rabbitMqPassword);
        });

        rabbitMq.ConfigureEndpoints(context);
    });
});

builder.Services.AddScoped<ILedgerAccountRepository, LedgerAccountRepository>();
builder.Services.AddScoped<ILedgerService, LedgerService.Services.LedgerService>();

await DatabaseBootstrapper.EnsureDatabaseAsync(ledgerDatabaseConnectionString);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LedgerDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.MapGet("/health", () => Results.Ok(new { service = "ms-ledger", transport = "RabbitMQ" }));

app.Run();
