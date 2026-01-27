using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificationService.Worker.Subscribers;

public class ProductCreatedSubscriber : RabbitMqSubscriber<ProductCreated>
{
    public ProductCreatedSubscriber(
        IOptions<RabbitMqOptions> options,
        IEventHandler<ProductCreated> handler,
        ServiceMetadata metadata,
        ILogger<RabbitMqSubscriber<ProductCreated>> logger)
        : base(options, handler, metadata, nameof(ProductCreated), logger)
    {
    }
}
