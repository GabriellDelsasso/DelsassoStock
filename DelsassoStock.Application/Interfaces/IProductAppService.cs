using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductItem> RegisterProduct(ProductViewModel productViewModel);
    }
}
