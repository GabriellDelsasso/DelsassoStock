using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Services;
using System.Text.RegularExpressions;

namespace DelsassoStock.Application.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly CustomerDomainService _customerDomainService;

        public CustomerAppService(CustomerDomainService customerDomainService)
        {
            _customerDomainService = customerDomainService;
        }

        public async Task<Client> RegisterCustomerAsync(CustomerViewModel customerViewModel)
        {
            if (customerViewModel != null)
            {
                try
                {
                    var validator = new CustomerViewModelValidator();
                    var validationResult = await validator.ValidateAsync(customerViewModel);

                    if (!validationResult.IsValid)
                        throw new Exception($"{validationResult.Errors}");

                    var clearCpf = Regex.Replace(customerViewModel.Cpf, @"[^\d]", "");

                    Client client = new Client
                    {
                        Id = Guid.NewGuid(),
                        Name = customerViewModel.Name,
                        Cpf = clearCpf
                    };

                    var result = await _customerDomainService.RegisterCustomerAsync(client);

                    if (result)
                        return client;
                    throw new Exception("Failure to register customer.");
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while registering the customer.", ex);
                }
            }
            throw new ArgumentNullException(nameof(customerViewModel), "CustomerViewModel cannot be null.");
        }

        public async Task<IEnumerable<Client>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerDomainService.GetAllCustomersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving customers.", ex);
            }
        }

        public async Task<bool> UpdateCustomerAsync(Guid idCustomer, CustomerViewModel customerViewModel)
        {
            if (customerViewModel != null)
            {
                try
                {
                    var validator = new CustomerViewModelValidator();
                    var validationResult = await validator.ValidateAsync(customerViewModel);

                    if (!validationResult.IsValid)
                        throw new Exception($"{validationResult.Errors}");

                    var existCustomer = await _customerDomainService.GetCustomerByIdAsync(idCustomer);

                    if (existCustomer == null)
                        return false;

                    var oldCpf = existCustomer.Cpf;

                    var clearCpf = Regex.Replace(customerViewModel.Cpf, @"[^\d]", "");

                    existCustomer.Name = customerViewModel.Name;
                    existCustomer.Cpf = clearCpf;

                    return await _customerDomainService.UpdateCustomerAsync(existCustomer, oldCpf);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating the customer.", ex);
                }
            }
            throw new ArgumentNullException(nameof(customerViewModel), "CustomerViewModel cannot be null.");
        }
    }
}
