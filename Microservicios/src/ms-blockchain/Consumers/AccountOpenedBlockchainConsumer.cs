using BlockchainService.Services;
using Contracts.Ledger;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class AccountOpenedBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<AccountOpenedEvent>
{
    public async Task Consume(ConsumeContext<AccountOpenedEvent> context)
    {
        await blockchainLedgerService.RecordAccountOpenedAsync(
            context.Message,
            context.CancellationToken);
    }
}
