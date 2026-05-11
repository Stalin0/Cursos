using ApiGateway.Dtos;

namespace ApiGateway.Services;

public interface IContactGatewayService
{
    Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyList<ContactResponse>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
