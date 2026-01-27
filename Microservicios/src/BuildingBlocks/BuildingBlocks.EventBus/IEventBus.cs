using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(EventEnvelope<T> evt, CancellationToken ct = default);
}
