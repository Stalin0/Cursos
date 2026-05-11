using Contracts.Contacts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuditService.Consumers;

public sealed class ContactCreatedAuditConsumer(ILogger<ContactCreatedAuditConsumer> logger)
    : IConsumer<ContactCreatedEvent>
{
    public Task Consume(ConsumeContext<ContactCreatedEvent> context)
    {
        logger.LogInformation(
            "AUDIT ContactCreated -> ContactId: {ContactId}, UserId: {UserId}, ContactName: {ContactName}",
            context.Message.ContactId,
            context.Message.UserId,
            context.Message.ContactName);

        return Task.CompletedTask;
    }
}
