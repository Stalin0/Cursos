using Microsoft.EntityFrameworkCore;
using Back.Models;

namespace Back.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("Categorias");

                entity.HasKey(c => c.CategoriaId);

                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");

                entity.HasKey(p => p.ProductoId);

                entity.Property(p => p.Nombre)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.Precio)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.Stock)
                    .IsRequired();

                entity.Property(p => p.Descripcion)
                    .HasMaxLength(500); 


                entity.HasOne<Categoria>()
                    .WithMany() 
                    .HasForeignKey(p => p.CategoriaId) 
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
