using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Customer;

namespace DelsassoStock.Domain.Services
{
    public class CustomerDomainService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerDomainService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> RegisterCustomerAsync(Client customer)
        {
            try
            {
                await _customerRepository.AddAsync(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
