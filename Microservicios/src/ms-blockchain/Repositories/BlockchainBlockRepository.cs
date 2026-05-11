using BlockchainService.Data;
using BlockchainService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlockchainService.Repositories;

public sealed class BlockchainBlockRepository(BlockchainDbContext dbContext) : IBlockchainBlockRepository
{
    public Task<BlockchainBlock?> GetLatestAsync(CancellationToken cancellationToken)
    {
        return dbContext.BlockchainBlocks
            .OrderByDescending(block => block.BlockNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task AddAsync(BlockchainBlock block, CancellationToken cancellationToken)
    {
        return dbContext.BlockchainBlocks.AddAsync(block, cancellationToken).AsTask();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
