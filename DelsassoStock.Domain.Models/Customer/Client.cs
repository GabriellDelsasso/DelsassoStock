namespace DelsassoStock.Domain.Models.Customer
{
    public class Client
    {
        public Client(Guid id, string name, string cpf)
        {
            Id = id;
            Name = name;
            Cpf = cpf;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = String.Empty;
        public string Cpf { get; private set; } = String.Empty;
    }
}
