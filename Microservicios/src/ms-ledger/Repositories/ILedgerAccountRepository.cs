using LedgerService.Entities;

namespace LedgerService.Repositories;

public interface ILedgerAccountRepository
{
    Task AddAsync(LedgerAccount account, CancellationToken cancellationToken);

    Task<LedgerAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<LedgerAccount?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken);

    Task SaveChangesAsync(CancellationToken cancellationToken);
}
