using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.EventBus;

public sealed class InMemoryEventBus : IEventBus
{
    private readonly ConcurrentQueue<object> _events = new();

    public Task PublishAsync<T>(EventEnvelope<T> evt, CancellationToken ct = default)
    {
        _events.Enqueue(evt);
        return Task.CompletedTask;
    }
}
