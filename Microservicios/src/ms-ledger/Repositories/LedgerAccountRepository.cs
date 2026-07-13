using LedgerService.Data;
using LedgerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerService.Repositories;

public sealed class LedgerAccountRepository(LedgerDbContext dbContext) : ILedgerAccountRepository
{
    public async Task AddAsync(LedgerAccount account, CancellationToken cancellationToken)
    {
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<LedgerAccount?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(account => account.Id == id, cancellationToken);
    }

    public Task<LedgerAccount?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Accounts
            .FirstOrDefaultAsync(account => account.Id == id, cancellationToken);
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
