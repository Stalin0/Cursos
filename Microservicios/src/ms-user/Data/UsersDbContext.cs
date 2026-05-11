using Microsoft.EntityFrameworkCore;
using UserService.Entities;

namespace UserService.Data;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.HasKey(user => user.Id);
            entity.Property(user => user.DocumentNumber).HasMaxLength(30).IsRequired();
            entity.Property(user => user.FirstName).HasMaxLength(100).IsRequired();
            entity.Property(user => user.LastName).HasMaxLength(100).IsRequired();
            entity.Property(user => user.Email).HasMaxLength(200).IsRequired();
            entity.Property(user => user.CreatedAt).IsRequired();
            entity.HasIndex(user => user.DocumentNumber).IsUnique();
            entity.HasIndex(user => user.Email).IsUnique();
        });
    }
}
