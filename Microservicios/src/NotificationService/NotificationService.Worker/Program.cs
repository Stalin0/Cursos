using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using Microsoft.Extensions.Options;
using NotificationService.Worker.Email;
using NotificationService.Worker.Handlers;
using NotificationService.Worker.Subscribers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<ServiceMetadata>(builder.Configuration.GetSection("ServiceMetadata"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ServiceMetadata>>().Value);

builder.Services.AddSingleton<IEmailSender, ConsoleEmailSender>();

builder.Services.AddSingleton<IEventHandler<UserCreated>, UserCreatedEmailHandler>();
builder.Services.AddSingleton<IEventHandler<ProductCreated>, ProductCreatedEmailHandler>();
builder.Services.AddSingleton<IEventHandler<SaleRegistered>, SaleRegisteredEmailHandler>();

builder.Services.AddHostedService<UserCreatedSubscriber>();
builder.Services.AddHostedService<ProductCreatedSubscriber>();
builder.Services.AddHostedService<SaleRegisteredSubscriber>();

var host = builder.Build();
host.Run();
