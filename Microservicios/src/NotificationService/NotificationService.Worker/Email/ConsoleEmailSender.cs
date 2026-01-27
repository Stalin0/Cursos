using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NotificationService.Worker.Email;

public class ConsoleEmailSender : IEmailSender
{
    private readonly ILogger<ConsoleEmailSender> _logger;

    public ConsoleEmailSender(ILogger<ConsoleEmailSender> logger)
    {
        _logger = logger;
    }

    public Task SendAsync(string to, string subject, string body, CancellationToken ct)
    {
        _logger.LogInformation("Email to {To} | {Subject} | {Body}", to, subject, body);
        return Task.CompletedTask;
    }
}
