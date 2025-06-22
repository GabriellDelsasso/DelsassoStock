namespace DelsassoStock.Application.ViewModels
{
    public class SaleResultViewModel
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalSale { get; set; }
        public List<SaleItemResultViewModel> Items { get; set; } = new();
    }

    public class SaleItemResultViewModel
    {
        public Guid ProductItemId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}