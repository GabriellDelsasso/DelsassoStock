using DelsassoStock.Domain.Models.Sale;

namespace DelsassoStock.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
    }
}
