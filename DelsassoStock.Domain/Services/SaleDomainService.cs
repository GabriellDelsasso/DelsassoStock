using DelsassoStock.Domain.Interfaces;
using DelsassoStock.Domain.Models.Sale;

namespace DelsassoStock.Domain.Services
{
    public class SaleDomainService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleDomainService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task AddSaleAsync(Sale sale)
        {
            try 
            {
                await _saleRepository.AddAsync(sale);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the sale.", ex);
            }
        }
    }
}
