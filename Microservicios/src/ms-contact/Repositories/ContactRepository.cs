using ContactService.Data;
using ContactService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Repositories;

public sealed class ContactRepository(ContactsDbContext dbContext) : IContactRepository
{
    public async Task AddAsync(Contact contact, CancellationToken cancellationToken)
    {
        dbContext.Contacts.Add(contact);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Contact>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Contacts
            .AsNoTracking()
            .Where(contact => contact.UserId == userId)
            .OrderBy(contact => contact.ContactName)
            .ToListAsync(cancellationToken);
    }
}
