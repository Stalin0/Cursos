using System.Threading;
using System.Threading.Tasks;
using ProductService.Domain;

namespace ProductService.Application;

public interface IProductRepository
{
    Task AddAsync(Product product, CancellationToken ct);
    Task<Product?> GetAsync(ProductId id, CancellationToken ct);
}
