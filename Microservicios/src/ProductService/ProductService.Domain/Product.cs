using System;

namespace ProductService.Domain;

public record ProductId(Guid Value);

public class Product
{
    public ProductId Id { get; }
    public string Name { get; }
    public Guid UserId { get; }

    public Product(ProductId id, string name, Guid userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
    }
}
