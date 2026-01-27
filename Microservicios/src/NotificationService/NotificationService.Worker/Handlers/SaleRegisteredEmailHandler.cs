using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using NotificationService.Worker.Email;

namespace NotificationService.Worker.Handlers;

public class SaleRegisteredEmailHandler : IEventHandler<SaleRegistered>
{
    private readonly IEmailSender _emailSender;

    public SaleRegisteredEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task HandleAsync(EventEnvelope<SaleRegistered> evt, CancellationToken ct)
    {
        var subject = $"Sale registered: {evt.Payload.SaleId}";
        var body = $"Sale for product {evt.Payload.ProductId} amount {evt.Payload.Amount}.";
        return _emailSender.SendAsync("sales@local", subject, body, ct);
    }
}
