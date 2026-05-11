using ContactService.Dtos;
using ContactService.Entities;
using ContactService.Repositories;

namespace ContactService.Services;

public sealed class ContactService(IContactRepository contactRepository) : IContactService
{
    public async Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            ContactName = request.ContactName,
            Phone = request.Phone,
            Email = request.Email,
            CreatedAt = DateTime.UtcNow
        };

        await contactRepository.AddAsync(contact, cancellationToken);

        return MapToResponse(contact);
    }

    public async Task<IReadOnlyList<ContactResponse>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var contacts = await contactRepository.GetByUserIdAsync(userId, cancellationToken);
        return contacts.Select(MapToResponse).ToList();
    }

    private static ContactResponse MapToResponse(Contact contact)
    {
        return new ContactResponse(
            contact.Id,
            contact.UserId,
            contact.ContactName,
            contact.Phone,
            contact.Email,
            contact.CreatedAt);
    }
}
