using Contracts.Ledger;
using LedgerService.Dtos;
using LedgerService.Services;
using MassTransit;

namespace LedgerService.Consumers;

public sealed class OpenAccountConsumer(ILedgerService ledgerService) : IConsumer<OpenAccountCommand>
{
    public async Task Consume(ConsumeContext<OpenAccountCommand> context)
    {
        var account = await ledgerService.OpenAccountAsync(
            new OpenAccountRequest(
                context.Message.AccountNumber,
                context.Message.OwnerName,
                context.Message.Currency),
            context.CancellationToken);

        await context.Publish(new AccountOpenedEvent(
            account.Id,
            account.AccountNumber,
            account.OwnerName,
            account.Currency,
            account.Balance,
            account.CreatedAt));

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
