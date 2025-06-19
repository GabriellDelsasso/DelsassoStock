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
                        return null;

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
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while registering the product.", ex);
                }
            }
            return null;
        }
    }
}
