namespace Contracts.Ledger;

public sealed record OpenAccountCommand(
    string AccountNumber,
    string OwnerName,
    string Currency);

public sealed record GetAccountByIdQuery(Guid AccountId);

public sealed record DepositFundsCommand(
    Guid AccountId,
    decimal Amount,
    string Reference);

public sealed record TransferFundsCommand(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    string Reference);

public sealed record AccountResult(
    bool Found,
    Guid? AccountId,
    string? AccountNumber,
    string? OwnerName,
    string? Currency,
    decimal Balance,
    DateTime? CreatedAt);

public sealed record TransferResult(
    bool Success,
    Guid? FromAccountId,
    Guid? ToAccountId,
    decimal Amount,
    decimal? FromBalance,
    decimal? ToBalance,
    string? Reference,
    DateTime? ProcessedAt,
    string? Error);

public sealed record AccountOpenedEvent(
    Guid AccountId,
    string AccountNumber,
    string OwnerName,
    string Currency,
    decimal Balance,
    DateTime CreatedAt);

public sealed record FundsDepositedEvent(
    Guid AccountId,
    decimal Amount,
    decimal NewBalance,
    string Reference,
    DateTime OccurredAt);

public sealed record FundsTransferredEvent(
    Guid FromAccountId,
    Guid ToAccountId,
    decimal Amount,
    decimal FromBalance,
    decimal ToBalance,
    string Reference,
    DateTime OccurredAt);
