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
    }
}
