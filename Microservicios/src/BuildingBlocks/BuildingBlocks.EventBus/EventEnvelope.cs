using System;

namespace BuildingBlocks.EventBus;

public record EventEnvelope<T>(
    string EventId,
    string EventType,
    DateTime OccurredAtUtc,
    string CorrelationId,
    ServiceMetadata Metadata,
    T Payload);
