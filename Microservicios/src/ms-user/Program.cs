using MassTransit;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Consumers;
using UserService.Repositories;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var rabbitMqHost = builder.Configuration["RabbitMq:Host"] ?? "127.0.0.1";
var rabbitMqVirtualHost = builder.Configuration["RabbitMq:VirtualHost"] ?? "/";
var rabbitMqUsername = builder.Configuration["RabbitMq:Username"] ?? "guest";
var rabbitMqPassword = builder.Configuration["RabbitMq:Password"] ?? "guest";

builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("UsersDatabase")));

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<CreateUserConsumer>();
    configurator.AddConsumer<GetUserByIdConsumer>();

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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, global::UserService.Services.UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<UsersDbContext>();
    await dbContext.Database.EnsureCreatedAsync();
}

app.MapGet("/health", () => Results.Ok(new { service = "ms-user", transport = "RabbitMQ" }));

app.Run();
