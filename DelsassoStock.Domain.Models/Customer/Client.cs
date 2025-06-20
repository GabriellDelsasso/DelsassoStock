namespace DelsassoStock.Domain.Models.Customer
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Cpf { get; set; } = String.Empty;
    }
}
