using Contracts.Ledger;
using LedgerService.Dtos;
using LedgerService.Services;
using MassTransit;

namespace LedgerService.Consumers;

public sealed class DepositFundsConsumer(ILedgerService ledgerService) : IConsumer<DepositFundsCommand>
{
    public async Task Consume(ConsumeContext<DepositFundsCommand> context)
    {
        var account = await ledgerService.DepositAsync(
            new DepositFundsRequest(
                context.Message.AccountId,
                context.Message.Amount,
                context.Message.Reference),
            context.CancellationToken);

        if (account is null)
        {
            await context.RespondAsync(new AccountResult(false, null, null, null, null, 0m, null));
            return;
        }

        await context.Publish(new FundsDepositedEvent(
            account.Id,
            context.Message.Amount,
            account.Balance,
            context.Message.Reference,
            DateTime.UtcNow));

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
