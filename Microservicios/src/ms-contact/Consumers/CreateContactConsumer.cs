using ContactService.Dtos;
using ContactService.Services;
using Contracts.Contacts;
using MassTransit;

namespace ContactService.Consumers;

public sealed class CreateContactConsumer(IContactService contactService) : IConsumer<CreateContactCommand>
{
    public async Task Consume(ConsumeContext<CreateContactCommand> context)
    {
        var message = context.Message;
        var contact = await contactService.CreateAsync(
            new CreateContactRequest(
                message.UserId,
                message.ContactName,
                message.Phone,
                message.Email),
            context.CancellationToken);

        await context.Publish(new ContactCreatedEvent(
            contact.Id,
            contact.UserId,
            contact.ContactName,
            contact.Phone,
            contact.Email,
            contact.CreatedAt));

        await context.RespondAsync(new ContactItem(
            contact.Id,
            contact.UserId,
            contact.ContactName,
            contact.Phone,
            contact.Email,
            contact.CreatedAt));
    }
}
