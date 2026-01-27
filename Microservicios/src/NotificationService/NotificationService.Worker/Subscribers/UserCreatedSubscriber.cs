using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificationService.Worker.Subscribers;

public class UserCreatedSubscriber : RabbitMqSubscriber<UserCreated>
{
    public UserCreatedSubscriber(
        IOptions<RabbitMqOptions> options,
        IEventHandler<UserCreated> handler,
        ServiceMetadata metadata,
        ILogger<RabbitMqSubscriber<UserCreated>> logger)
        : base(options, handler, metadata, nameof(UserCreated), logger)
    {
    }
}
