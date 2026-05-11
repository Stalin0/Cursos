using System.ComponentModel.DataAnnotations;

namespace ContactService.Dtos;

public sealed record CreateContactRequest(
    [Required] Guid UserId,
    [Required, MaxLength(150)] string ContactName,
    [Required, MaxLength(30)] string Phone,
    [EmailAddress, MaxLength(200)] string? Email);
