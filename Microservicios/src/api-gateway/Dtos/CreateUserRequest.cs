using System.ComponentModel.DataAnnotations;

namespace ApiGateway.Dtos;

public sealed record CreateUserRequest(
    [Required, MaxLength(30)] string DocumentNumber,
    [Required, MaxLength(100)] string FirstName,
    [Required, MaxLength(100)] string LastName,
    [Required, EmailAddress, MaxLength(200)] string Email);
