namespace Contracts.Users;

public sealed record CreateUserCommand(
    string DocumentNumber,
    string FirstName,
    string LastName,
    string Email);

public sealed record GetUserByIdQuery(Guid UserId);

public sealed record UserResult(
    bool Found,
    Guid? Id,
    string? DocumentNumber,
    string? FirstName,
    string? LastName,
    string? Email,
    DateTime? CreatedAt);

public sealed record UserCreatedEvent(
    Guid UserId,
    string DocumentNumber,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt);
