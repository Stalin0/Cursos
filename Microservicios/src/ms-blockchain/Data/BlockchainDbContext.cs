using BlockchainService.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlockchainService.Data;

public sealed class BlockchainDbContext(DbContextOptions<BlockchainDbContext> options) : DbContext(options)
{
    public DbSet<BlockchainBlock> BlockchainBlocks => Set<BlockchainBlock>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlockchainBlock>(entity =>
        {
            entity.ToTable("blockchain_blocks");
            entity.HasKey(block => block.Id);

            entity.Property(block => block.EventType).HasMaxLength(80).IsRequired();
            entity.Property(block => block.AggregateId).IsRequired();
            entity.Property(block => block.PayloadJson).HasColumnType("text").IsRequired();
            entity.Property(block => block.PayloadHash).HasMaxLength(64).IsRequired();
            entity.Property(block => block.PreviousBlockHash).HasMaxLength(64).IsRequired();
            entity.Property(block => block.BlockHash).HasMaxLength(64).IsRequired();
            entity.Property(block => block.BlockNumber).IsRequired();
            entity.Property(block => block.SourceCreatedAt).IsRequired();
            entity.Property(block => block.RecordedAt).IsRequired();

            entity.HasIndex(block => block.BlockNumber).IsUnique();
            entity.HasIndex(block => block.BlockHash).IsUnique();
            entity.HasIndex(block => new { block.EventType, block.AggregateId });
        });
    }
}
