namespace LedgerService.Dtos;

public sealed record TransferResponse(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    decimal FromBalance,
    decimal ToBalance,
    string Reference,
    DateTime ProcessedAt);
