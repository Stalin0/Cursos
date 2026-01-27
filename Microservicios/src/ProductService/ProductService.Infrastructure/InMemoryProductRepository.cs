using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using ProductService.Application;
using ProductService.Domain;

namespace ProductService.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private readonly ConcurrentDictionary<Guid, Product> _storage = new();

    public Task AddAsync(Product product, CancellationToken ct)
    {
        _storage[product.Id.Value] = product;
        return Task.CompletedTask;
    }

    public Task<Product?> GetAsync(ProductId id, CancellationToken ct)
    {
        _storage.TryGetValue(id.Value, out var product);
        return Task.FromResult(product);
    }
}
