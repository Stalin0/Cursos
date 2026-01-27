using System;

namespace UserService.Domain;

public record UserId(Guid Value);

public class User
{
    public UserId Id { get; }
    public string Name { get; }
    public string Email { get; }

    public User(UserId id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}
