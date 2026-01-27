using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using NotificationService.Worker.Email;

namespace NotificationService.Worker.Handlers;

public class ProductCreatedEmailHandler : IEventHandler<ProductCreated>
{
    private readonly IEmailSender _emailSender;

    public ProductCreatedEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task HandleAsync(EventEnvelope<ProductCreated> evt, CancellationToken ct)
    {
        var subject = $"Product created: {evt.Payload.Name}";
        var body = $"Product {evt.Payload.ProductId} linked to user {evt.Payload.UserId}.";
        return _emailSender.SendAsync("ops@local", subject, body, ct);
    }
}
