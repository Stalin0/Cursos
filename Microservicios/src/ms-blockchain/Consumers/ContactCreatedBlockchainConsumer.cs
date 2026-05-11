using BlockchainService.Services;
using Contracts.Contacts;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class ContactCreatedBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<ContactCreatedEvent>
{
    public async Task Consume(ConsumeContext<ContactCreatedEvent> context)
    {
        await blockchainLedgerService.RecordContactCreatedAsync(
            context.Message,
            context.CancellationToken);
    }
}
