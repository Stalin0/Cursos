using ApiGateway.Dtos;

namespace ApiGateway.Services;

public sealed class UserContactAggregatorService(
    IUserGatewayService userGatewayService,
    IContactGatewayService contactGatewayService) : IUserContactAggregatorService
{
    public async Task<UserContactResponse?> GetUserWithContactsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var userTask = userGatewayService.GetByIdAsync(userId, cancellationToken);
        var contactsTask = contactGatewayService.GetByUserIdAsync(userId, cancellationToken);

        await Task.WhenAll(userTask, contactsTask);

        var user = userTask.Result;
        if (user is null)
        {
            return null;
        }

        var contacts = contactsTask.Result
            .Select(contact => new UserContactItemResponse(
                $"USR-{user.Id}-CNT-{contact.Id}",
                contact.Id,
                contact.ContactName,
                contact.Phone,
                contact.Email))
            .ToList();

        return new UserContactResponse(
            user.Id,
            $"{user.FirstName} {user.LastName}",
            user.Email,
            contacts);
    }
}
