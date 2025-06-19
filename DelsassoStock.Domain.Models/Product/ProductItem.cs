namespace DelsassoStock.Domain.Models.Product
{
    public class ProductItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
    }
}
