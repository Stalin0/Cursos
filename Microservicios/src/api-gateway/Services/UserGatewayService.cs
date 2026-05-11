using ApiGateway.Dtos;
using Contracts.Users;
using MassTransit;

namespace ApiGateway.Services;

public sealed class UserGatewayService(IRequestClient<CreateUserCommand> createUserClient,
    IRequestClient<GetUserByIdQuery> getUserByIdClient) : IUserGatewayService
{
    public async Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await createUserClient.GetResponse<UserResult>(
            new CreateUserCommand(
                request.DocumentNumber,
                request.FirstName,
                request.LastName,
                request.Email),
            cancellationToken);

        return MapRequired(response.Message);
    }

    public async Task<UserResponse?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var response = await getUserByIdClient.GetResponse<UserResult>(
            new GetUserByIdQuery(userId),
            cancellationToken);

        return response.Message.Found ? MapRequired(response.Message) : null;
    }

    private static UserResponse MapRequired(UserResult result)
    {
        return new UserResponse(
            result.Id!.Value,
            result.DocumentNumber!,
            result.FirstName!,
            result.LastName!,
            result.Email!,
            result.CreatedAt!.Value);
    }
}
