using BlockchainService.Services;
using Contracts.Users;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class UserCreatedBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        await blockchainLedgerService.RecordUserCreatedAsync(
            context.Message,
            context.CancellationToken);
    }
}
