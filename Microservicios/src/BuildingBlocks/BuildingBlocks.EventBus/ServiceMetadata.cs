namespace BuildingBlocks.EventBus;

public sealed class ServiceMetadata
{
    public string ServiceName { get; init; } = string.Empty;
    public string TenantId { get; init; } = string.Empty;
    public string Environment { get; init; } = string.Empty;
}
