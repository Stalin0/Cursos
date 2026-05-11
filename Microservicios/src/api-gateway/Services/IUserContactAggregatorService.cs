using ApiGateway.Dtos;

namespace ApiGateway.Services;

public interface IUserContactAggregatorService
{
    Task<UserContactResponse?> GetUserWithContactsAsync(Guid userId, CancellationToken cancellationToken);
}
