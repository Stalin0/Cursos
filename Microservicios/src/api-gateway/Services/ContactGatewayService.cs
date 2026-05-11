using ApiGateway.Dtos;
using Contracts.Contacts;
using MassTransit;

namespace ApiGateway.Services;

public sealed class ContactGatewayService(
    IRequestClient<CreateContactCommand> createContactClient,
    IRequestClient<GetContactsByUserIdQuery> getContactsByUserIdClient) : IContactGatewayService
{
    public async Task<ContactResponse> CreateAsync(CreateContactRequest request, CancellationToken cancellationToken)
    {
        var response = await createContactClient.GetResponse<ContactItem>(
            new CreateContactCommand(
                request.UserId,
                request.ContactName,
                request.Phone,
                request.Email),
            cancellationToken);

        return Map(response.Message);
    }

    public async Task<IReadOnlyList<ContactResponse>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var response = await getContactsByUserIdClient.GetResponse<ContactsResult>(
            new GetContactsByUserIdQuery(userId),
            cancellationToken);

        return response.Message.Contacts.Select(Map).ToList();
    }

    private static ContactResponse Map(ContactItem contact)
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
