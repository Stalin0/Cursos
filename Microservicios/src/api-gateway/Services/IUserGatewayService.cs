using ApiGateway.Dtos;

namespace ApiGateway.Services;

public interface IUserGatewayService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken);

    Task<UserResponse?> GetByIdAsync(Guid userId, CancellationToken cancellationToken);

    Task<UserResponse?> UpdateAsync(Guid userId, UpdateUserRequest request, CancellationToken cancellationToken);
}
