using Contracts.Ledger;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuditService.Consumers;

public sealed class AccountOpenedAuditConsumer(ILogger<AccountOpenedAuditConsumer> logger) : IConsumer<AccountOpenedEvent>
{
    public Task Consume(ConsumeContext<AccountOpenedEvent> context)
    {
        logger.LogInformation(
            "AUDIT AccountOpened -> AccountId: {AccountId}, AccountNumber: {AccountNumber}, OwnerName: {OwnerName}, Currency: {Currency}",
            context.Message.AccountId,
            context.Message.AccountNumber,
            context.Message.OwnerName,
            context.Message.Currency);

        return Task.CompletedTask;
    }
}
