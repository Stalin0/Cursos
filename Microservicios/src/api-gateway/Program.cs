using ApiGateway.Services;
using Contracts.Contacts;
using Contracts.Ledger;
using Contracts.Users;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.SetRabbitMqReplyToRequestClientFactory();

    configurator.AddRequestClient<CreateUserCommand>();
    configurator.AddRequestClient<GetUserByIdQuery>();
    configurator.AddRequestClient<UpdateUserCommand>();
    configurator.AddRequestClient<CreateContactCommand>();
    configurator.AddRequestClient<GetContactsByUserIdQuery>();
    configurator.AddRequestClient<OpenAccountCommand>();
    configurator.AddRequestClient<GetAccountByIdQuery>();
    configurator.AddRequestClient<DepositFundsCommand>();
    configurator.AddRequestClient<TransferFundsCommand>();

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

builder.Services.AddScoped<IUserGatewayService, UserGatewayService>();
builder.Services.AddScoped<IContactGatewayService, ContactGatewayService>();
builder.Services.AddScoped<ILedgerGatewayService, LedgerGatewayService>();
builder.Services.AddScoped<IUserContactAggregatorService, UserContactAggregatorService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
