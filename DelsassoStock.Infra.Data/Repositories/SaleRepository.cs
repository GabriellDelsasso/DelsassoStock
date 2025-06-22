using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Sale;
using DelsassoStock.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelsassoStock.Infra.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Sale>> GetAllAsync()
        {
            return await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.Items)
                    .ThenInclude(i => i.ProductItem)
                .ToListAsync();
        }
    }
}
