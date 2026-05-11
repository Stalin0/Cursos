using Contracts.Users;
using MassTransit;
using UserService.Services;

namespace UserService.Consumers;

public sealed class GetUserByIdConsumer(IUserService userService) : IConsumer<GetUserByIdQuery>
{
    public async Task Consume(ConsumeContext<GetUserByIdQuery> context)
    {
        var user = await userService.GetByIdAsync(context.Message.UserId, context.CancellationToken);

        if (user is null)
        {
            await context.RespondAsync(new UserResult(false, null, null, null, null, null, null));
            return;
        }

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
