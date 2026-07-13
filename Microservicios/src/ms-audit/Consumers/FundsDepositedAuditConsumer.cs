using Contracts.Ledger;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuditService.Consumers;

public sealed class FundsDepositedAuditConsumer(ILogger<FundsDepositedAuditConsumer> logger) : IConsumer<FundsDepositedEvent>
{
    public Task Consume(ConsumeContext<FundsDepositedEvent> context)
    {
        logger.LogInformation(
            "AUDIT FundsDeposited -> AccountId: {AccountId}, Amount: {Amount}, NewBalance: {NewBalance}, Reference: {Reference}",
            context.Message.AccountId,
            context.Message.Amount,
            context.Message.NewBalance,
            context.Message.Reference);

        return Task.CompletedTask;
    }
}
