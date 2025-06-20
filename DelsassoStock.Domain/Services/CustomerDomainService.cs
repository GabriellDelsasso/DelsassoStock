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
                var cpfExists = await _customerRepository.CpfExistsAsync(customer.Cpf);

                if (cpfExists)
                    return false;

                await _customerRepository.AddAsync(customer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Client>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllAsync();
            }
            catch
            {
                return Enumerable.Empty<Client>();
            }
        }
    }
}
