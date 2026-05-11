namespace ApiGateway.Dtos;

public sealed record ContactResponse(
    Guid Id,
    Guid UserId,
    string ContactName,
    string Phone,
    string? Email,
    DateTime CreatedAt);
