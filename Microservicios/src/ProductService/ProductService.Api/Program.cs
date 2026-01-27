using BuildingBlocks.EventBus;
using Microsoft.Extensions.Options;
using ProductService.Application;
using ProductService.Domain;
using ProductService.Infrastructure;

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

builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<CreateProductUseCase>();

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

app.MapPost("/products", async (
    CreateProductRequest request,
    CreateProductUseCase useCase,
    ServiceMetadata metadata,
    HttpContext context) =>
{
    var result = await useCase.ExecuteAsync(request, metadata, context.RequestAborted);
    return Results.Created($"/products/{result.ProductId}", result);
})
.WithSummary("Create product")
.WithDescription("Objective: register a new product and publish the ProductCreated event.");

app.MapGet("/products/{id:guid}", async (
    Guid id,
    IProductRepository repository,
    HttpContext context) =>
{
    var product = await repository.GetAsync(new ProductId(id), context.RequestAborted);
    return product is null
        ? Results.NotFound()
        : Results.Ok(new CreateProductResult(product.Id.Value, product.Name, product.UserId));
})
.WithSummary("Get product by id")
.WithDescription("Objective: retrieve product details for UI and sales flows.");

app.Run();
