using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using NotificationService.Worker.Email;

namespace NotificationService.Worker.Handlers;

public class UserCreatedEmailHandler : IEventHandler<UserCreated>
{
    private readonly IEmailSender _emailSender;

    public UserCreatedEmailHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public Task HandleAsync(EventEnvelope<UserCreated> evt, CancellationToken ct)
    {
        var subject = $"Welcome {evt.Payload.Name}";
        var body = $"User created in {evt.Metadata.ServiceName} with tenant {evt.Metadata.TenantId}.";
        return _emailSender.SendAsync(evt.Payload.Email, subject, body, ct);
    }
}
