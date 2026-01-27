using System;

namespace SalesService.Domain;

public record SaleId(Guid Value);

public class Sale
{
    public SaleId Id { get; }
    public Guid ProductId { get; }
    public Guid UserId { get; }
    public decimal Amount { get; }

    public Sale(SaleId id, Guid productId, Guid userId, decimal amount)
    {
        Id = id;
        ProductId = productId;
        UserId = userId;
        Amount = amount;
    }
}
