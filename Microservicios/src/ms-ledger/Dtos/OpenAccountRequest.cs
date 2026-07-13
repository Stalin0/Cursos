using System.ComponentModel.DataAnnotations;

namespace LedgerService.Dtos;

public sealed record OpenAccountRequest(
    [Required, MaxLength(30)] string AccountNumber,
    [Required, MaxLength(150)] string OwnerName,
    [Required, MaxLength(10)] string Currency);
