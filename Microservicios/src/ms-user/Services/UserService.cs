using UserService.Dtos;
using UserService.Entities;
using UserService.Repositories;

namespace UserService.Services;

public sealed class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            DocumentNumber = request.DocumentNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        await userRepository.AddAsync(user, cancellationToken);

        return MapToResponse(user);
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);
        return user is null ? null : MapToResponse(user);
    }

    public async Task<UserResponse?> UpdateAsync(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdForUpdateAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return null;
        }

        user.DocumentNumber = request.DocumentNumber;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await userRepository.SaveChangesAsync(cancellationToken);

        return MapToResponse(user);
    }

    private static UserResponse MapToResponse(User user)
    {
        return new UserResponse(
            user.Id,
            user.DocumentNumber,
            user.FirstName,
            user.LastName,
            user.Email,
            user.CreatedAt);
    }
}
