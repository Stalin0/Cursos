using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using UserService.Application;
using UserService.Domain;

namespace UserService.Infrastructure;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _storage = new();

    public Task AddAsync(User user, CancellationToken ct)
    {
        _storage[user.Id.Value] = user;
        return Task.CompletedTask;
    }

    public Task<User?> GetAsync(UserId id, CancellationToken ct)
    {
        _storage.TryGetValue(id.Value, out var user);
        return Task.FromResult(user);
    }
}
