using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificationService.Worker.Subscribers;

public class SaleRegisteredSubscriber : RabbitMqSubscriber<SaleRegistered>
{
    public SaleRegisteredSubscriber(
        IOptions<RabbitMqOptions> options,
        IEventHandler<SaleRegistered> handler,
        ServiceMetadata metadata,
        ILogger<RabbitMqSubscriber<SaleRegistered>> logger)
        : base(options, handler, metadata, nameof(SaleRegistered), logger)
    {
    }
}
