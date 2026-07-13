using UserService.Dtos;

namespace UserService.Services;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);

    Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<UserResponse?> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken);
}
