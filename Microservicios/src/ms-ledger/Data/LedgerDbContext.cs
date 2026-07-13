using LedgerService.Entities;
using Microsoft.EntityFrameworkCore;

namespace LedgerService.Data;

public sealed class LedgerDbContext(DbContextOptions<LedgerDbContext> options) : DbContext(options)
{
    public DbSet<LedgerAccount> Accounts => Set<LedgerAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LedgerAccount>(entity =>
        {
            entity.ToTable("ledger_accounts");
            entity.HasKey(account => account.Id);
            entity.Property(account => account.AccountNumber).HasMaxLength(30).IsRequired();
            entity.Property(account => account.OwnerName).HasMaxLength(150).IsRequired();
            entity.Property(account => account.Currency).HasMaxLength(10).IsRequired();
            entity.Property(account => account.Balance).HasPrecision(18, 2).IsRequired();
            entity.Property(account => account.CreatedAt).IsRequired();
            entity.HasIndex(account => account.AccountNumber).IsUnique();
        });
    }
}
