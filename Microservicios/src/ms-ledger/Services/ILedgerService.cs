using LedgerService.Dtos;

namespace LedgerService.Services;

public interface ILedgerService
{
    Task<LedgerAccountResponse> OpenAccountAsync(OpenAccountRequest request, CancellationToken cancellationToken);

    Task<LedgerAccountResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<LedgerAccountResponse?> DepositAsync(DepositFundsRequest request, CancellationToken cancellationToken);

    Task<TransferResponse?> TransferAsync(TransferFundsRequest request, CancellationToken cancellationToken);
}
