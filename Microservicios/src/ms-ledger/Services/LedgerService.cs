using LedgerService.Dtos;
using LedgerService.Entities;
using LedgerService.Repositories;

namespace LedgerService.Services;

public sealed class LedgerService(ILedgerAccountRepository ledgerAccountRepository) : ILedgerService
{
    public async Task<LedgerAccountResponse> OpenAccountAsync(OpenAccountRequest request, CancellationToken cancellationToken)
    {
        var account = new LedgerAccount
        {
            Id = Guid.NewGuid(),
            AccountNumber = request.AccountNumber,
            OwnerName = request.OwnerName,
            Currency = request.Currency.ToUpperInvariant(),
            Balance = 0m,
            CreatedAt = DateTime.UtcNow
        };

        await ledgerAccountRepository.AddAsync(account, cancellationToken);
        return Map(account);
    }

    public async Task<LedgerAccountResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await ledgerAccountRepository.GetByIdAsync(id, cancellationToken);
        return account is null ? null : Map(account);
    }

    public async Task<LedgerAccountResponse?> DepositAsync(DepositFundsRequest request, CancellationToken cancellationToken)
    {
        var account = await ledgerAccountRepository.GetByIdForUpdateAsync(request.AccountId, cancellationToken);
        if (account is null)
        {
            return null;
        }

        account.Balance += request.Amount;
        await ledgerAccountRepository.SaveChangesAsync(cancellationToken);
        return Map(account);
    }

    public async Task<TransferResponse?> TransferAsync(TransferFundsRequest request, CancellationToken cancellationToken)
    {
        if (request.FromAccountId == request.ToAccountId)
        {
            throw new InvalidOperationException("Source and destination accounts must be different.");
        }

        var fromAccount = await ledgerAccountRepository.GetByIdForUpdateAsync(request.FromAccountId, cancellationToken);
        var toAccount = await ledgerAccountRepository.GetByIdForUpdateAsync(request.ToAccountId, cancellationToken);

        if (fromAccount is null || toAccount is null)
        {
            return null;
        }

        if (!string.Equals(fromAccount.Currency, toAccount.Currency, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Accounts must use the same currency.");
        }

        if (fromAccount.Balance < request.Amount)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        fromAccount.Balance -= request.Amount;
        toAccount.Balance += request.Amount;

        await ledgerAccountRepository.SaveChangesAsync(cancellationToken);

        return new TransferResponse(
            fromAccount.Id,
            toAccount.Id,
            request.Amount,
            fromAccount.Balance,
            toAccount.Balance,
            request.Reference,
            DateTime.UtcNow);
    }

    private static LedgerAccountResponse Map(LedgerAccount account)
    {
        return new LedgerAccountResponse(
            account.Id,
            account.AccountNumber,
            account.OwnerName,
            account.Currency,
            account.Balance,
            account.CreatedAt);
    }
}
