using ContactService.Services;
using Contracts.Contacts;
using MassTransit;

namespace ContactService.Consumers;

public sealed class GetContactsByUserIdConsumer(IContactService contactService) : IConsumer<GetContactsByUserIdQuery>
{
    public async Task Consume(ConsumeContext<GetContactsByUserIdQuery> context)
    {
        var contacts = await contactService.GetByUserIdAsync(
            context.Message.UserId,
            context.CancellationToken);

        var response = contacts
            .Select(contact => new ContactItem(
                contact.Id,
                contact.UserId,
                contact.ContactName,
                contact.Phone,
                contact.Email,
                contact.CreatedAt))
            .ToList();

        await context.RespondAsync(new ContactsResult(context.Message.UserId, response));
    }
}
