using Contracts.Ledger;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuditService.Consumers;

public sealed class FundsTransferredAuditConsumer(ILogger<FundsTransferredAuditConsumer> logger) : IConsumer<FundsTransferredEvent>
{
    public Task Consume(ConsumeContext<FundsTransferredEvent> context)
    {
        logger.LogInformation(
            "AUDIT FundsTransferred -> FromAccountId: {FromAccountId}, ToAccountId: {ToAccountId}, Amount: {Amount}, Reference: {Reference}",
            context.Message.FromAccountId,
            context.Message.ToAccountId,
            context.Message.Amount,
            context.Message.Reference);

        return Task.CompletedTask;
    }
}
