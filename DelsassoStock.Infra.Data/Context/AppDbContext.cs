using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Models.Product;
using DelsassoStock.Domain.Models.Sale;
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
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItem { get; set; }

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

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasOne(s => s.Customer)
                      .WithMany()
                      .HasForeignKey(s => s.CustomerId);
                entity.Property(s => s.TotalSale).HasPrecision(10, 2);
            });

            modelBuilder.Entity<SaleItem>(entity =>
            {
                entity.HasKey(si => si.Id);
                entity.HasOne(si => si.Sale)
                      .WithMany(s => s.Items)
                      .HasForeignKey(si => si.SaleId);
                entity.HasOne(si => si.ProductItem)
                      .WithMany()
                      .HasForeignKey(si => si.ProductItemId);
                entity.Property(si => si.UnitPrice).HasPrecision(10, 2);
            });
        }
    }
}
