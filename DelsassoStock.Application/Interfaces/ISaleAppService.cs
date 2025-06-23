using DelsassoStock.Application.ViewModels;

namespace DelsassoStock.Application.Interfaces
{
    public interface ISaleAppService
    {
        Task<bool> CreateSale(SaleViewModel saleViewModel);

        Task<List<SaleResultViewModel>> GetAllSalesAsync();

        Task<bool> UpdateSale(Guid saleId, SaleViewModel saleViewModel);

        Task<bool> DeleteSale(Guid saleId);
    }
}
