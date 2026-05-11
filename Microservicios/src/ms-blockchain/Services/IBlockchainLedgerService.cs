using BlockchainService.Entities;
using Contracts.Contacts;
using Contracts.Users;

namespace BlockchainService.Services;

public interface IBlockchainLedgerService
{
    Task<BlockchainBlock> RecordUserCreatedAsync(UserCreatedEvent message, CancellationToken cancellationToken);

    Task<BlockchainBlock> RecordContactCreatedAsync(ContactCreatedEvent message, CancellationToken cancellationToken);
}
