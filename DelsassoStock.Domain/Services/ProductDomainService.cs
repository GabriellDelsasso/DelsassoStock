using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Domain.Services
{
    public class ProductDomainService
    {
        private readonly IProductRepository _productRepository;

        public ProductDomainService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> RegisterProductAsync(ProductItem productItem)
        {
            try
            {
                await _productRepository.AddAsync(productItem);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
