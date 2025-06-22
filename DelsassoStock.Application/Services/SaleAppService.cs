using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Models.Product;
using DelsassoStock.Domain.Models.Sale;
using DelsassoStock.Domain.Services;

namespace DelsassoStock.Application.Services
{
    public class SaleAppService : ISaleAppService
    {
        private readonly SaleDomainService _saleDomainService;
        private readonly CustomerDomainService _customerDomainService;
        private readonly ProductDomainService _productDomainService;

        public SaleAppService(SaleDomainService saleDomainService, CustomerDomainService customerDomainService, ProductDomainService productDomainService)
        {
            _saleDomainService = saleDomainService;
            _customerDomainService = customerDomainService;
            _productDomainService = productDomainService;
        }

        public async Task<bool> CreateSale(SaleViewModel saleViewModel)
        {
            Client? customer = null;

            if (saleViewModel.CustomerId.HasValue)
            {
                customer = await _customerDomainService.GetCustomerByIdAsync(saleViewModel.CustomerId.Value);
                if (customer == null) 
                    return false;
            }

            var saleItems = new List<SaleItem>();
            decimal totalSale = 0;

            foreach (var item in saleViewModel.Items)
            {
                var product = await _productDomainService.GetProductByIdAsync(item.ProductItemId);
                if (product == null) continue;

                if (product.Quantity < item.Quantity)
                    return false;

                product.Quantity -= item.Quantity;
                await _productDomainService.UpdateProductAsync(product);

                var saleItem = new SaleItem
                {
                    Id = Guid.NewGuid(),
                    ProductItemId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                saleItems.Add(saleItem);
                totalSale += product.Price * item.Quantity;
            }

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                CustomerId = customer?.Id,
                TotalSale = totalSale,
                Items = saleItems
            };

            await _saleDomainService.AddSaleAsync(sale);
            return true;
        }
    }
}
