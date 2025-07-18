﻿using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Application.Interfaces
{
    public interface IProductAppService
    {
        Task<ProductItem> RegisterProduct(ProductViewModel productViewModel);

        Task<IEnumerable<ProductItem>> GetAllProducts();

        Task<bool> UpdateProduct(Guid idProduct, ProductViewModel productViewModel);

        Task<bool> DeleteProductAsync(Guid id);
    }
}
