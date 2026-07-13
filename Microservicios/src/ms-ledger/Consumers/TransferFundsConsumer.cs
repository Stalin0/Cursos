using Contracts.Ledger;
using LedgerService.Dtos;
using LedgerService.Services;
using MassTransit;

namespace LedgerService.Consumers;

public sealed class TransferFundsConsumer(ILedgerService ledgerService) : IConsumer<TransferFundsCommand>
{
    public async Task Consume(ConsumeContext<TransferFundsCommand> context)
    {
        try
        {
            var transfer = await ledgerService.TransferAsync(
                new TransferFundsRequest(
                    context.Message.FromAccountId,
                    context.Message.ToAccountId,
                    context.Message.Amount,
                    context.Message.Reference),
                context.CancellationToken);

            if (transfer is null)
            {
                await context.RespondAsync(new TransferResult(
                    false,
                    context.Message.FromAccountId,
                    context.Message.ToAccountId,
                    context.Message.Amount,
                    null,
                    null,
                    context.Message.Reference,
                    null,
                    "Account not found."));
                return;
            }

            await context.Publish(new FundsTransferredEvent(
                transfer.FromAccountId,
                transfer.ToAccountId,
                transfer.Amount,
                transfer.FromBalance,
                transfer.ToBalance,
                transfer.Reference,
                transfer.ProcessedAt));

            await context.RespondAsync(new TransferResult(
                true,
                transfer.FromAccountId,
                transfer.ToAccountId,
                transfer.Amount,
                transfer.FromBalance,
                transfer.ToBalance,
                transfer.Reference,
                transfer.ProcessedAt,
                null));
        }
        catch (InvalidOperationException exception)
        {
            await context.RespondAsync(new TransferResult(
                false,
                context.Message.FromAccountId,
                context.Message.ToAccountId,
                context.Message.Amount,
                null,
                null,
                context.Message.Reference,
                null,
                exception.Message));
        }
    }
}
