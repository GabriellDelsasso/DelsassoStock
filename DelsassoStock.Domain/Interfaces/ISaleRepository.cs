using DelsassoStock.Domain.Models.Sale;

namespace DelsassoStock.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);

        Task<List<Sale>> GetAllAsync();

        Task<Sale?> GetByIdAsync(Guid id);

        Task UpdateAsync(Sale sale);

        Task RemoveSaleItemAsync(Sale sale, Guid updatedProductItemId);

        Task CreateSaleItemAsync(SaleItem saleItem);
    }
}
