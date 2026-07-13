using AuditService.Consumers;
using MassTransit;

var builder = Microsoft.Extensions.Hosting.Host.CreateApplicationBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<UserCreatedAuditConsumer>();
    configurator.AddConsumer<ContactCreatedAuditConsumer>();
    configurator.AddConsumer<AccountOpenedAuditConsumer>();
    configurator.AddConsumer<FundsDepositedAuditConsumer>();
    configurator.AddConsumer<FundsTransferredAuditConsumer>();

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

var application = builder.Build();
application.Run();
