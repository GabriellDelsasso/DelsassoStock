using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Customer;
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

        public async Task<List<SaleResultViewModel>> GetAllSalesAsync()
        {
            var sales = await _saleDomainService.GetAllSalesAsync();

            return sales.Select(s => new SaleResultViewModel
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                CustomerName = s.Customer?.Name,
                TotalSale = s.TotalSale,
                Items = s.Items.Select(i => new SaleItemResultViewModel
                {
                    ProductItemId = i.ProductItemId,
                    ProductName = i.ProductItem?.Name ?? "",
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            }).ToList();
        }

        public async Task<bool> UpdateSale(Guid saleId, SaleViewModel saleViewModel)
        {
            var sale = await _saleDomainService.GetSaleByIdAsync(saleId);
            if (sale == null)
                return false;

            sale.CustomerId = saleViewModel.CustomerId;

            var updatedItems = saleViewModel.Items.ToDictionary(i => i.ProductItemId);

            var newSaleItems = new List<SaleItem>();

            await UpdateOrRemoveExistingItemsAsync(sale, updatedItems);

            await AddNewItemsAsync(sale, updatedItems, newSaleItems);

            if (newSaleItems.Any())
                await _saleDomainService.CreateSaleItemAsync(newSaleItems);

            await _saleDomainService.UpdateSaleAsync(sale);
            return true;
        }

        public async Task<bool> DeleteSale(Guid saleId)
        {
            var sale = await _saleDomainService.GetSaleByIdAsync(saleId);

            if (sale == null)
                return false;

            foreach (var item in sale.Items)
            {
                var product = await _productDomainService.GetProductByIdAsync(item.ProductItemId);
                if (product != null)
                {
                    product.Quantity += item.Quantity;
                    await _productDomainService.UpdateProductAsync(product);
                }
            }
            await _saleDomainService.DeleteSaleAsync(saleId);
            return true;
        }

        private async Task UpdateOrRemoveExistingItemsAsync(Sale sale, Dictionary<Guid, SaleItemViewModel> updatedItems)
        {
            for (int i = sale.Items.Count - 1; i >= 0; i--)
            {
                var existingItem = sale.Items[i];

                if (updatedItems.TryGetValue(existingItem.ProductItemId, out var updatedItem))
                {
                    var product = await _productDomainService.GetProductByIdAsync(existingItem.ProductItemId);
                    if (product == null)
                        continue;

                    int diff = updatedItem.Quantity - existingItem.Quantity;

                    if (diff > 0)
                    {
                        if (product.Quantity < diff)
                            throw new InvalidOperationException("Not enough stock for the product.");

                        product.Quantity -= diff;
                        await _productDomainService.UpdateProductAsync(product);
                    }
                    else if (diff < 0)
                    {
                        product.Quantity += Math.Abs(diff);
                        await _productDomainService.UpdateProductAsync(product);
                    }

                    existingItem.Quantity = updatedItem.Quantity;
                    existingItem.UnitPrice = product.Price;

                    updatedItems.Remove(existingItem.ProductItemId);
                }
                else
                {
                    var product = await _productDomainService.GetProductByIdAsync(existingItem.ProductItemId);
                    if (product != null)
                    {
                        product.Quantity += existingItem.Quantity;
                        await _productDomainService.UpdateProductAsync(product);
                    }

                    await _saleDomainService.RemoveSaleItemAsync(sale, existingItem.Id);
                }
            }
        }

        private async Task AddNewItemsAsync(Sale sale, Dictionary<Guid, SaleItemViewModel> updatedItems, List<SaleItem> newSaleItems)
        {
            foreach (var newItem in updatedItems.Values)
            {
                var product = await _productDomainService.GetProductByIdAsync(newItem.ProductItemId);
                if (product == null) continue;

                if (product.Quantity < newItem.Quantity)
                    throw new InvalidOperationException("Not enough stock for the product.");

                product.Quantity -= newItem.Quantity;
                await _productDomainService.UpdateProductAsync(product);

                var saleItem = new SaleItem
                {
                    Id = Guid.NewGuid(),
                    SaleId = sale.Id,
                    ProductItemId = product.Id,
                    Quantity = newItem.Quantity,
                    UnitPrice = product.Price
                };

                sale.Items.Add(saleItem);
                newSaleItems.Add(saleItem);
            }

            sale.TotalSale = sale.Items.Sum(i => i.UnitPrice * i.Quantity);
        }
    }
}
