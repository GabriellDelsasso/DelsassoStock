using DelsassoStock.Domain.Models.Customer;
using DelsassoStock.Domain.Models.Product;

namespace DelsassoStock.Domain.Models.Sale
{
    public class Sale
    {
        public Sale(Guid id, List<ProductItem> products, Client customer, Decimal totalSale)
        {
            Id = id;
            Products = products;
            Customer = customer;
            TotalSale = totalSale;
        }

        public Guid Id { get; set; }
        public List<ProductItem> Products { get; set; }
        public Client Customer { get; set; }
        public Decimal TotalSale { get; set; }
    }
}
