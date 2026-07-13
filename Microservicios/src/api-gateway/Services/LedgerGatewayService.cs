using ApiGateway.Dtos;
using Contracts.Ledger;
using MassTransit;

namespace ApiGateway.Services;

public sealed class LedgerGatewayService(
    IRequestClient<OpenAccountCommand> openAccountClient,
    IRequestClient<GetAccountByIdQuery> getAccountByIdClient,
    IRequestClient<DepositFundsCommand> depositFundsClient,
    IRequestClient<TransferFundsCommand> transferFundsClient) : ILedgerGatewayService
{
    public async Task<LedgerAccountResponse> OpenAccountAsync(OpenAccountRequest request, CancellationToken cancellationToken)
    {
        var response = await openAccountClient.GetResponse<AccountResult>(
            new OpenAccountCommand(request.AccountNumber, request.OwnerName, request.Currency),
            cancellationToken);

        return MapRequired(response.Message);
    }

    public async Task<LedgerAccountResponse?> GetAccountByIdAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var response = await getAccountByIdClient.GetResponse<AccountResult>(
            new GetAccountByIdQuery(accountId),
            cancellationToken);

        return response.Message.Found ? MapRequired(response.Message) : null;
    }

    public async Task<LedgerAccountResponse?> DepositAsync(Guid accountId, DepositFundsRequest request, CancellationToken cancellationToken)
    {
        var response = await depositFundsClient.GetResponse<AccountResult>(
            new DepositFundsCommand(accountId, request.Amount, request.Reference),
            cancellationToken);

        return response.Message.Found ? MapRequired(response.Message) : null;
    }

    public async Task<(TransferResponse? Transfer, string? Error)> TransferAsync(
        TransferFundsRequest request,
        CancellationToken cancellationToken)
    {
        var response = await transferFundsClient.GetResponse<TransferResult>(
            new TransferFundsCommand(request.FromAccountId, request.ToAccountId, request.Amount, request.Reference),
            cancellationToken);

        if (!response.Message.Success)
        {
            return (null, response.Message.Error);
        }

        return (new TransferResponse(
            response.Message.FromAccountId!.Value,
            response.Message.ToAccountId!.Value,
            response.Message.Amount,
            response.Message.FromBalance!.Value,
            response.Message.ToBalance!.Value,
            response.Message.Reference!,
            response.Message.ProcessedAt!.Value), null);
    }

    private static LedgerAccountResponse MapRequired(AccountResult result)
    {
        return new LedgerAccountResponse(
            result.AccountId!.Value,
            result.AccountNumber!,
            result.OwnerName!,
            result.Currency!,
            result.Balance,
            result.CreatedAt!.Value);
    }
}
