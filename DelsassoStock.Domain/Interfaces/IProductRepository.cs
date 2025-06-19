using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(ProductItem produto);

        Task<IEnumerable<ProductItem>> GetAllAsync();

        Task UpdateAsync(ProductItem produto);

        Task<ProductItem> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
