using BuildingBlocks.EventBus;
using Microsoft.Extensions.Options;
using SalesService.Application;
using SalesService.Domain;
using SalesService.Infrastructure;

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

builder.Services.AddSingleton<ISaleRepository, InMemorySaleRepository>();
builder.Services.AddScoped<RegisterSaleUseCase>();

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

app.MapPost("/sales", async (
    RegisterSaleRequest request,
    RegisterSaleUseCase useCase,
    ServiceMetadata metadata,
    HttpContext context) =>
{
    var result = await useCase.ExecuteAsync(request, metadata, context.RequestAborted);
    return Results.Created($"/sales/{result.SaleId}", result);
})
.WithSummary("Register sale")
.WithDescription("Objective: create a sale record and publish the SaleRegistered event.");

app.MapGet("/sales/{id:guid}", async (
    Guid id,
    ISaleRepository repository,
    HttpContext context) =>
{
    var sale = await repository.GetAsync(new SaleId(id), context.RequestAborted);
    return sale is null
        ? Results.NotFound()
        : Results.Ok(new RegisterSaleResult(sale.Id.Value, sale.ProductId, sale.UserId, sale.Amount));
})
.WithSummary("Get sale by id")
.WithDescription("Objective: retrieve sale details for UI and reporting.");

app.Run();
