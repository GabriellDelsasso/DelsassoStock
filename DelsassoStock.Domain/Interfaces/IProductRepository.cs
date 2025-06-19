using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task AddAsync(ProductItem produto);
    }
}
