using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Product;
using DelsassoStock.Infra.Data.Context;

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
    }
}
