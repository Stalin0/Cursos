namespace ApiGateway.Dtos;

public sealed record LedgerAccountResponse(
    Guid Id,
    string AccountNumber,
    string OwnerName,
    string Currency,
    decimal Balance,
    DateTime CreatedAt);
