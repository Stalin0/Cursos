using System.Threading;
using System.Threading.Tasks;
using UserService.Domain;

namespace UserService.Application;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken ct);
    Task<User?> GetAsync(UserId id, CancellationToken ct);
}
