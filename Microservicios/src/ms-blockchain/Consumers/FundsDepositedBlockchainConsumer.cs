using BlockchainService.Services;
using Contracts.Ledger;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class FundsDepositedBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<FundsDepositedEvent>
{
    public async Task Consume(ConsumeContext<FundsDepositedEvent> context)
    {
        await blockchainLedgerService.RecordFundsDepositedAsync(
            context.Message,
            context.CancellationToken);
    }
}
