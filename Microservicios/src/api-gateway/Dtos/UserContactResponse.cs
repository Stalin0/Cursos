namespace ApiGateway.Dtos;

public sealed record UserContactResponse(
    Guid UserId,
    string FullName,
    string Email,
    IReadOnlyList<UserContactItemResponse> Contacts);

public sealed record UserContactItemResponse(
    string UserContactKey,
    Guid ContactId,
    string ContactName,
    string Phone,
    string? Email);
