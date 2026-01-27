using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus;

public interface IEventHandler<T>
{
    Task HandleAsync(EventEnvelope<T> evt, CancellationToken ct);
}
