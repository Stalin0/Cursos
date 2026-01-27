using System.Threading;
using System.Threading.Tasks;
using SalesService.Domain;

namespace SalesService.Application;

public interface ISaleRepository
{
    Task AddAsync(Sale sale, CancellationToken ct);
    Task<Sale?> GetAsync(SaleId id, CancellationToken ct);
}
