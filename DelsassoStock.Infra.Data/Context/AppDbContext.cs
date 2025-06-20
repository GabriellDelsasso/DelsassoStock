using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace DelsassoStock.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductItem> Products { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductItem>(entity =>
            {
                entity.Property(p => p.Id).IsRequired();
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Quantity).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(c => c.Id).IsRequired();
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Cpf).IsRequired().HasMaxLength(14);
            });
        }
    }
}
