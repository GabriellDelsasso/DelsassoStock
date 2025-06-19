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

        public async Task<IEnumerable<ProductItem>> GetAllProductsAsync()
        {
            try
            {
                return await _productRepository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<ProductItem>();
            }
        }

        public async Task<bool> UpdateProductAsync(ProductItem productItem)
        {
            try
            {
                await _productRepository.UpdateAsync(productItem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductItem?> GetProductByIdAsync(Guid id)
        {
            try
            {
                return await _productRepository.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                await _productRepository.DeleteAsync(id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
