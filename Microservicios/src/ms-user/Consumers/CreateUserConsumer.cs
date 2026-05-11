using Contracts.Users;
using MassTransit;
using UserService.Dtos;
using UserService.Services;

namespace UserService.Consumers;

public sealed class CreateUserConsumer(IUserService userService) : IConsumer<CreateUserCommand>
{
    public async Task Consume(ConsumeContext<CreateUserCommand> context)
    {
        var message = context.Message;
        var user = await userService.CreateAsync(
            new CreateUserRequest(
                message.DocumentNumber,
                message.FirstName,
                message.LastName,
                message.Email),
            context.CancellationToken);

        await context.Publish(new UserCreatedEvent(
            user.Id,
            user.DocumentNumber,
            user.FirstName,
            user.LastName,
            user.Email,
            user.CreatedAt));

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
