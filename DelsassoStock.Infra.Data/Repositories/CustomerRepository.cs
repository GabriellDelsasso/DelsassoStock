using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DelsassoStock.Infra.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Client customer)
        {
            await _context.Clients.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CpfExistsAsync(string cpf)
        {
            return await _context.Clients.AnyAsync(c => c.Cpf == cpf);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task UpdateAsync(Client customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            var customer = await _context.Clients.FindAsync(id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found.");

            return customer;
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Clients.FindAsync(id);
            if (customer != null)
            {
                _context.Clients.Remove(customer);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Customer not found.");
            }
        }
    }
}
