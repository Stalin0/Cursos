using ContactService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Data;

public sealed class ContactsDbContext(DbContextOptions<ContactsDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("contacts");
            entity.HasKey(contact => contact.Id);
            entity.Property(contact => contact.UserId).IsRequired();
            entity.Property(contact => contact.ContactName).HasMaxLength(150).IsRequired();
            entity.Property(contact => contact.Phone).HasMaxLength(30).IsRequired();
            entity.Property(contact => contact.Email).HasMaxLength(200);
            entity.Property(contact => contact.CreatedAt).IsRequired();
            entity.HasIndex(contact => contact.UserId);
        });
    }
}
