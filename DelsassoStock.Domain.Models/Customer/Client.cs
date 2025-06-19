namespace DelsassoStock.Domain.Models.Customer
{
    public class Client
    {
        public Client(Guid id, string name, string cpf)
        {
            Id = id;
            Name = name;
            CPF = cpf;
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
    }
}
