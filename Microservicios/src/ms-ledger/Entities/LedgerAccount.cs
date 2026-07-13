namespace LedgerService.Entities;

public sealed class LedgerAccount
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public string OwnerName { get; set; } = string.Empty;

    public string Currency { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }
}
