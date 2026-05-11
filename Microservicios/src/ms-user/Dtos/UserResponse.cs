namespace UserService.Dtos;

public sealed record UserResponse(
    Guid Id,
    string DocumentNumber,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt);
