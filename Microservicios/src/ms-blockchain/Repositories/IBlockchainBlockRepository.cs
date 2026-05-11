using BlockchainService.Entities;

namespace BlockchainService.Repositories;

public interface IBlockchainBlockRepository
{
    Task<BlockchainBlock?> GetLatestAsync(CancellationToken cancellationToken);

    Task AddAsync(BlockchainBlock block, CancellationToken cancellationToken);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
