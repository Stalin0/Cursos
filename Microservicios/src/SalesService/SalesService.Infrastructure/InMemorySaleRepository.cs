using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using SalesService.Application;
using SalesService.Domain;

namespace SalesService.Infrastructure;

public class InMemorySaleRepository : ISaleRepository
{
    private readonly ConcurrentDictionary<Guid, Sale> _storage = new();

    public Task AddAsync(Sale sale, CancellationToken ct)
    {
        _storage[sale.Id.Value] = sale;
        return Task.CompletedTask;
    }

    public Task<Sale?> GetAsync(SaleId id, CancellationToken ct)
    {
        _storage.TryGetValue(id.Value, out var sale);
        return Task.FromResult(sale);
    }
}
