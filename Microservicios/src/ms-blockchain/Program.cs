using BlockchainService.Consumers;
using BlockchainService.Data;
using BlockchainService.Providers;
using BlockchainService.Repositories;
using BlockchainService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";
var blockchainDatabaseConnectionString = builder.Configuration.GetConnectionString("BlockchainDatabase")
    ?? throw new InvalidOperationException("BlockchainDatabase connection string is required.");

builder.Services.AddDbContext<BlockchainDbContext>(options =>
    options.UseNpgsql(blockchainDatabaseConnectionString));

builder.Services.AddScoped<IBlockchainBlockRepository, BlockchainBlockRepository>();
builder.Services.AddScoped<IBlockHashProvider, Sha256BlockHashProvider>();
builder.Services.AddScoped<IBlockchainLedgerService, BlockchainLedgerService>();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<UserCreatedBlockchainConsumer>();
    configurator.AddConsumer<UserUpdatedBlockchainConsumer>();
    configurator.AddConsumer<ContactCreatedBlockchainConsumer>();
    configurator.AddConsumer<AccountOpenedBlockchainConsumer>();
    configurator.AddConsumer<FundsDepositedBlockchainConsumer>();
    configurator.AddConsumer<FundsTransferredBlockchainConsumer>();

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

await DatabaseBootstrapper.EnsureDatabaseAsync(blockchainDatabaseConnectionString);

var application = builder.Build();

using (var scope = application.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BlockchainDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

application.Run();
