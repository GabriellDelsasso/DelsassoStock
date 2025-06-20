using DelsassoStock.Domain.Models.Customer;

namespace DelsassoStock.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Client customer);

        Task<bool> CpfExistsAsync(string cpf);

        Task<IEnumerable<Client>> GetAllAsync();

        Task UpdateAsync(Client cutomer);

        Task<Client> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);
    }
}
