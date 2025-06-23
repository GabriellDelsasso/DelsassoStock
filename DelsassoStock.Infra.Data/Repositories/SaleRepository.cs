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

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale != null)
            {
                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Product not found.");
            }
        }

        public async Task RemoveSaleItemAsync(Sale sale, Guid updatedProductItemId)
        {
            var itemToRemove = sale.Items
                .Where(item => item.Id == updatedProductItemId);

            if (itemToRemove.Any())
            {
                _context.SaleItem.RemoveRange(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateSaleItemAsync(SaleItem saleItem)
        {
            _context.SaleItem.Add(saleItem);
            await _context.SaveChangesAsync();
        }
    }
}
