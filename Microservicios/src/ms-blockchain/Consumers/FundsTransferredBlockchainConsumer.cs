using BlockchainService.Services;
using Contracts.Ledger;
using MassTransit;

namespace BlockchainService.Consumers;

public sealed class FundsTransferredBlockchainConsumer(IBlockchainLedgerService blockchainLedgerService)
    : IConsumer<FundsTransferredEvent>
{
    public async Task Consume(ConsumeContext<FundsTransferredEvent> context)
    {
        await blockchainLedgerService.RecordFundsTransferredAsync(
            context.Message,
            context.CancellationToken);
    }
}
