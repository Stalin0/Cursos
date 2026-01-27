using BuildingBlocks.EventBus;
using Microsoft.Extensions.Options;
using UserService.Application;
using UserService.Domain;
using UserService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<ServiceMetadata>(builder.Configuration.GetSection("ServiceMetadata"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<ServiceMetadata>>().Value);

var useRabbitMq = builder.Configuration.GetValue<bool>("EventBus:UseRabbitMq");
if (useRabbitMq)
{
    builder.Services.AddSingleton<IEventBus, RabbitMqEventBus>();
}
else
{
    builder.Services.AddSingleton<IEventBus, InMemoryEventBus>();
}

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<CreateUserUseCase>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, ct) =>
    {
        document.Servers.Clear();
        return Task.CompletedTask;
    });
});

var app = builder.Build();

app.MapOpenApi();

app.MapPost("/users", async (
    CreateUserRequest request,
    CreateUserUseCase useCase,
    ServiceMetadata metadata,
    HttpContext context) =>
{
    var result = await useCase.ExecuteAsync(request, metadata, context.RequestAborted);
    return Results.Created($"/users/{result.UserId}", result);
})
.WithSummary("Create user")
.WithDescription("Objective: register a new user and publish the UserCreated event.");

app.MapGet("/users/{id:guid}", async (
    Guid id,
    IUserRepository repository,
    HttpContext context) =>
{
    var user = await repository.GetAsync(new UserId(id), context.RequestAborted);
    return user is null
        ? Results.NotFound()
        : Results.Ok(new CreateUserResult(user.Id.Value, user.Name, user.Email));
})
.WithSummary("Get user by id")
.WithDescription("Objective: retrieve user details for UI and integrations.");

app.Run();
