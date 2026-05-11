namespace Contracts.Contacts;

public sealed record CreateContactCommand(
    Guid UserId,
    string ContactName,
    string Phone,
    string? Email);

public sealed record GetContactsByUserIdQuery(Guid UserId);

public sealed record ContactItem(
    Guid Id,
    Guid UserId,
    string ContactName,
    string Phone,
    string? Email,
    DateTime CreatedAt);

public sealed record ContactsResult(
    Guid UserId,
    IReadOnlyList<ContactItem> Contacts);

public sealed record ContactCreatedEvent(
    Guid ContactId,
    Guid UserId,
    string ContactName,
    string Phone,
    string? Email,
    DateTime CreatedAt);
