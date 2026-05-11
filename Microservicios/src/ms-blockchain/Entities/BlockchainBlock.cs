namespace BlockchainService.Entities;

public sealed class BlockchainBlock
{
    public Guid Id { get; set; }

    public int BlockNumber { get; set; }

    public string EventType { get; set; } = string.Empty;

    public Guid AggregateId { get; set; }

    public string PayloadJson { get; set; } = string.Empty;

    public string PayloadHash { get; set; } = string.Empty;

    public string PreviousBlockHash { get; set; } = string.Empty;

    public string BlockHash { get; set; } = string.Empty;

    public DateTime SourceCreatedAt { get; set; }

    public DateTime RecordedAt { get; set; }
}
