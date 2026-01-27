using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using SalesService.Domain;

namespace SalesService.Application;

public record RegisterSaleRequest(Guid ProductId, Guid UserId, decimal Amount);

public record RegisterSaleResult(Guid SaleId, Guid ProductId, Guid UserId, decimal Amount);

public class RegisterSaleUseCase
{
    private readonly ISaleRepository _repository;
    private readonly IEventBus _eventBus;

    public RegisterSaleUseCase(ISaleRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<RegisterSaleResult> ExecuteAsync(
        RegisterSaleRequest request,
        ServiceMetadata metadata,
        CancellationToken ct)
    {
        var sale = new Sale(new SaleId(Guid.NewGuid()), request.ProductId, request.UserId, request.Amount);
        await _repository.AddAsync(sale, ct);

        var evt = EventEnvelopeFactory.Create(
            new SaleRegistered(sale.Id.Value, sale.ProductId, sale.UserId, sale.Amount),
            metadata);

        await _eventBus.PublishAsync(evt, ct);

        return new RegisterSaleResult(sale.Id.Value, sale.ProductId, sale.UserId, sale.Amount);
    }
}
