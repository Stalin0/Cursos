using ContactService.Entities;

namespace ContactService.Repositories;

public interface IContactRepository
{
    Task AddAsync(Contact contact, CancellationToken cancellationToken);

    Task<IReadOnlyList<Contact>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
