using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Dtos;

public sealed record DepositFundsRequest(
    [Range(0.01, double.MaxValue)] decimal Amount,
    [Required, MaxLength(120)] string Reference);
