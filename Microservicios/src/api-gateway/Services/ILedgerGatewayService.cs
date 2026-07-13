using ApiGateway.Dtos;

namespace ApiGateway.Services;

public interface ILedgerGatewayService
{
    Task<LedgerAccountResponse> OpenAccountAsync(OpenAccountRequest request, CancellationToken cancellationToken);

    Task<LedgerAccountResponse?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken);

    Task<LedgerAccountResponse?> DepositAsync(Guid accountId, DepositFundsRequest request, CancellationToken cancellationToken);

    Task<(TransferResponse? Transfer, string? Error)> TransferAsync(TransferFundsRequest request, CancellationToken cancellationToken);
}
