using DelsassoStock.Application.ViewModels;

namespace DelsassoStock.Application.Interfaces
{
    public interface ISaleAppService
    {
        Task<bool> CreateSale(SaleViewModel saleViewModel);

        Task<List<SaleResultViewModel>> GetAllSalesAsync();
    }
}
