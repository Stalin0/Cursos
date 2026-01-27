using System;

namespace BuildingBlocks.EventBus;

public static class EventEnvelopeFactory
{
    public static EventEnvelope<T> Create<T>(T payload, ServiceMetadata metadata, string? correlationId = null)
    {
        return new EventEnvelope<T>(
            EventId: Guid.NewGuid().ToString(),
            EventType: typeof(T).Name,
            OccurredAtUtc: DateTime.UtcNow,
            CorrelationId: correlationId ?? Guid.NewGuid().ToString(),
            Metadata: metadata,
            Payload: payload);
    }
}
