using System;
using System.Threading;
using System.Threading.Tasks;
using BuildingBlocks.Contracts;
using BuildingBlocks.EventBus;
using UserService.Domain;

namespace UserService.Application;

public record CreateUserRequest(string Name, string Email);

public record CreateUserResult(Guid UserId, string Name, string Email);

public class CreateUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IEventBus _eventBus;

    public CreateUserUseCase(IUserRepository repository, IEventBus eventBus)
    {
        _repository = repository;
        _eventBus = eventBus;
    }

    public async Task<CreateUserResult> ExecuteAsync(
        CreateUserRequest request,
        ServiceMetadata metadata,
        CancellationToken ct)
    {
        var user = new User(new UserId(Guid.NewGuid()), request.Name, request.Email);
        await _repository.AddAsync(user, ct);

        var evt = EventEnvelopeFactory.Create(
            new UserCreated(user.Id.Value, user.Name, user.Email),
            metadata);

        await _eventBus.PublishAsync(evt, ct);

        return new CreateUserResult(user.Id.Value, user.Name, user.Email);
    }
}
