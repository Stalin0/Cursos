using ContactService.Data;
using ContactService.Consumers;
using ContactService.Repositories;
using ContactService.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";

builder.Services.AddDbContext<ContactsDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ContactsDatabase")));

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CreateContactConsumer>();
    configurator.AddConsumer<GetContactsByUserIdConsumer>();

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

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, global::ContactService.Services.ContactService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.MapGet("/health", () => Results.Ok(new { service = "ms-contact", transport = "RabbitMQ" }));

app.Run();
