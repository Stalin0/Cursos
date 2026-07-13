using Contracts.Ledger;
using LedgerService.Services;
using MassTransit;

namespace LedgerService.Consumers;

public sealed class GetAccountByIdConsumer(ILedgerService ledgerService) : IConsumer<GetAccountByIdQuery>
{
    public async Task Consume(ConsumeContext<GetAccountByIdQuery> context)
    {
        var account = await ledgerService.GetByIdAsync(context.Message.AccountId, context.CancellationToken);
        if (account is null)
        {
            await context.RespondAsync(new AccountResult(false, null, null, null, null, 0m, null));
            return;
        }

        await context.RespondAsync(new AccountResult(
            true,
            account.Id,
            account.AccountNumber,
            account.OwnerName,
            account.Currency,
            account.Balance,
            account.CreatedAt));
    }
}
