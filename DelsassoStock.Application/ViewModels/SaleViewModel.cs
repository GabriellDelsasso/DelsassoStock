namespace DelsassoStock.Application.ViewModels
{
    public class SaleViewModel
    {
        public Guid? CustomerId { get; set; }
        public List<SaleItemViewModel> Items { get; set; } = new();
    }

    public class SaleItemViewModel
    {
        public Guid ProductItemId { get; set; }
        public int Quantity { get; set; }
    }
}
