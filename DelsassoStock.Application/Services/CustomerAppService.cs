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

                    var client = new Client(Guid.NewGuid(), customerViewModel.Name, clearCpf);

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
    }
}
