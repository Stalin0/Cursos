using ContactService.Dtos;

namespace ContactService.Services;

public interface IContactService
{
    Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken);

    Task<IReadOnlyList<ContactResponse>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
