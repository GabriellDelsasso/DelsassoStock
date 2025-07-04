﻿using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Customer;

namespace DelsassoStock.Application.Interfaces
{
    public interface ICustomerAppService
    {
        Task<Client> RegisterCustomerAsync(CustomerViewModel customerViewModel);

        Task<IEnumerable<Client>> GetAllCustomersAsync();

        Task<bool> UpdateCustomerAsync(Guid idCustomer, CustomerViewModel customerViewModel);

        Task<bool> DeleteCustomerAsync(Guid idCustomer);
    }
}
