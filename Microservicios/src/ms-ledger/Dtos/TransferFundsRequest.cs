using System.ComponentModel.DataAnnotations;

namespace LedgerService.Dtos;

public sealed record TransferFundsRequest(
    [Required] Guid FromAccountId,
    [Required] Guid ToAccountId,
    [Range(0.01, double.MaxValue)] decimal Amount,
    [Required, MaxLength(120)] string Reference);
