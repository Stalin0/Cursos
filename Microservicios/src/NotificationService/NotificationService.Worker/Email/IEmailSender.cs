using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.Worker.Email;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string body, CancellationToken ct);
}
