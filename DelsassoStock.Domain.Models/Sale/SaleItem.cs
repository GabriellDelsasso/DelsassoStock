namespace DelsassoStock.Domain.Models.Sale
{
    public class SaleItem
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Sale? Sale { get; set; }
        public Product.ProductItem? ProductItem { get; set; }
    }
}
