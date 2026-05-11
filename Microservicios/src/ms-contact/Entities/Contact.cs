namespace ContactService.Entities;

public sealed class Contact
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string ContactName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }
}
