using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Product;
using DelsassoStock.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelsassoStock.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProductItem produto)
        {
            await _context.Products.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductItem>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task UpdateAsync(ProductItem produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<ProductItem> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return product;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Product not found.");
            }
        }
    }
}
