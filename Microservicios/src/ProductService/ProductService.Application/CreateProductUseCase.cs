using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using ProductService.Domain;

namespace ProductService.Application;

public record CreateProductRequest(string Name, Guid UserId);

public record CreateProductResult(Guid ProductId, string Name, Guid UserId);

public class CreateProductUseCase
{
    private readonly IProductRepository _repository;
    private readonly IEventBus _eventBus;

    public CreateProductUseCase(IProductRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<CreateProductResult> ExecuteAsync(
        CreateProductRequest request,
        ServiceMetadata metadata,
        CancellationToken ct)
    {
        var product = new Product(new ProductId(Guid.NewGuid()), request.Name, request.UserId);
        await _repository.AddAsync(product, ct);

        var evt = EventEnvelopeFactory.Create(
            new ProductCreated(product.Id.Value, product.Name, product.UserId),
            metadata);

        await _eventBus.PublishAsync(evt, ct);

        return new CreateProductResult(product.Id.Value, product.Name, product.UserId);
    }
}
