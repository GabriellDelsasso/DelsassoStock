using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Product;
using DelsassoStock.Domain.Services;

namespace DelsassoStock.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly ProductDomainService _productDomainService;
        public ProductAppService(ProductDomainService productDomainService)
        {
            _productDomainService = productDomainService;
        }

        public async Task<ProductItem> RegisterProduct(ProductViewModel productViewModel)
        {
            if(productViewModel != null)
            {
                try 
                {
                    var validator = new ProductViewModelValidator();
                    var validationResult = await validator.ValidateAsync(productViewModel);

                    if (!validationResult.IsValid)
                        throw new Exception("Validation failed.");

                    ProductItem productItem = new ProductItem
                    {
                        Id = Guid.NewGuid(),
                        Name = productViewModel.Name,
                        Quantity = productViewModel.Quantity,
                        Price = productViewModel.Price
                    };

                    var result = await _productDomainService.RegisterProductAsync(productItem);

                    if (result)
                        return productItem;
                    throw new Exception("Failure to register product.");
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while registering the product.", ex);
                }
            }
            throw new ArgumentNullException(nameof(productViewModel), "ProductViewModel cannot be null.");
        }

        public async Task<IEnumerable<ProductItem>> GetAllProducts()
        {
            try
            {
                return await _productDomainService.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<bool> UpdateProduct(Guid idProduct, ProductViewModel productViewModel)
        {
            if (productViewModel != null)
            {
                try
                {
                    var validator = new ProductViewModelValidator();
                    var validationResult = await validator.ValidateAsync(productViewModel);

                    if (!validationResult.IsValid)
                        return false;

                    var productItem = await _productDomainService.GetProductByIdAsync(idProduct);

                    if (productItem == null)
                        return false;

                    productItem.Name = productViewModel.Name;
                    productItem.Quantity = productViewModel.Quantity;
                    productItem.Price = productViewModel.Price;

                    return await _productDomainService.UpdateProductAsync(productItem);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the product.", ex);
                }
            }
            return false;
        }

        public async Task<bool> DeleteProductAsync(Guid idProduct)
        {
            try
            {
                return await _productDomainService.DeleteProductAsync(idProduct);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
        }
    }
}
