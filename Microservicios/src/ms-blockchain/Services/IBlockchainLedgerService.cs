using BlockchainService.Entities;
using Contracts.Contacts;
using Contracts.Ledger;
using Contracts.Users;

namespace BlockchainService.Services;

public interface IBlockchainLedgerService
{
    Task<BlockchainBlock> RecordUserCreatedAsync(UserCreatedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordUserUpdatedAsync(UserUpdatedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordContactCreatedAsync(ContactCreatedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordAccountOpenedAsync(AccountOpenedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordFundsDepositedAsync(FundsDepositedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordFundsTransferredAsync(FundsTransferredEvent message, CancellationToken cancellationToken);
}
