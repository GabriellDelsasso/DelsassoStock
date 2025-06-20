using DelsassoStock.Domain.Models.Customer;

namespace DelsassoStock.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Client customer);
    }
}
