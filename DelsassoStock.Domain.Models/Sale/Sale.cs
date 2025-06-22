using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Domain.Models.Sale
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Client? Customer { get; set; }
        public decimal TotalSale { get; set; }
        public List<SaleItem> Items { get; set; } = new();
    }
}
