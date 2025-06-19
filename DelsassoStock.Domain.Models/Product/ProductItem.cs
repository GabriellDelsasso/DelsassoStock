namespace DelsassoStock.Domain.Models.Product
{
    public class ProductItem
    {
        public ProductItem(Guid id, string name, Decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public Decimal Price { get; set; }
    }
}
