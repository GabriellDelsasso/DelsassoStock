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

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            try
            {
                return await _saleRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving sales.", ex);
            }
        }

        public async Task<Sale?> GetSaleByIdAsync(Guid saleId)
        {
            try
            {
                return await _saleRepository.GetByIdAsync(saleId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving the sale by ID.", ex);
            }
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            try
            {
                await _saleRepository.UpdateAsync(sale);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while updating the sale.", ex);
            }
        }

        public async Task RemoveSaleItemAsync(Sale sale, Guid updatedProductItemId)
        {
            try
            {
                await _saleRepository.RemoveSaleItemAsync(sale, updatedProductItemId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while removing sale items.", ex);
            }
        }

        public async Task CreateSaleItemAsync(List<SaleItem> saleItems)
        {
            try
            {
                foreach (var saleItem in saleItems)
                {
                    await _saleRepository.CreateSaleItemAsync(saleItem);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while creating the sale item.", ex);
            }
        }
    }
}
