using Contracts.Users;
using MassTransit;
using UserService.Dtos;
using UserService.Services;

namespace UserService.Consumers;

public sealed class UpdateUserConsumer(IUserService userService) : IConsumer<UpdateUserCommand>
{
    public async Task Consume(ConsumeContext<UpdateUserCommand> context)
    {
        var message = context.Message;
        var user = await userService.UpdateAsync(
            new UpdateUserRequest(
                message.UserId,
                message.DocumentNumber,
                message.FirstName,
                message.LastName,
                message.Email),
            context.CancellationToken);

        if (user is null)
        {
            await context.RespondAsync(new UserResult(false, null, null, null, null, null, null));
            return;
        }

        await context.Publish(new UserUpdatedEvent(
            user.Id,
            user.DocumentNumber,
            user.FirstName,
            user.LastName,
            user.Email,
            DateTime.UtcNow));

        await context.RespondAsync(new UserResult(
            true,
            user.Id,
            user.DocumentNumber,
            user.FirstName,
            user.LastName,
            user.Email,
            user.CreatedAt));
    }
}
