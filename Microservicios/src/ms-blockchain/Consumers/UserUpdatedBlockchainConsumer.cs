using BlockchainService.Services;
using Contracts.Users;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class UserUpdatedBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<UserUpdatedEvent>
{
    public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        await blockchainLedgerService.RecordUserUpdatedAsync(
            context.Message,
            context.CancellationToken);
    }
}
