using System.ComponentModel.DataAnnotations;

namespace LedgerService.Dtos;

public sealed record DepositFundsRequest(
    [Required] Guid AccountId,
    [Range(0.01, double.MaxValue)] decimal Amount,
    [Required, MaxLength(120)] string Reference);
