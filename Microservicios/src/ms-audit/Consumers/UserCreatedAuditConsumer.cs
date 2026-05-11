using Contracts.Users;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace AuditService.Consumers;

public sealed class UserCreatedAuditConsumer(ILogger<UserCreatedAuditConsumer> logger) : IConsumer<UserCreatedEvent>
{
    public Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        logger.LogInformation(
            "AUDIT UserCreated -> UserId: {UserId}, Email: {Email}, CreatedAt: {CreatedAt}",
            context.Message.UserId,
            context.Message.Email,
            context.Message.CreatedAt);

        return Task.CompletedTask;
    }
}
