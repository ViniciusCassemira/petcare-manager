namespace system_petshop.Models
{
    public class Client: User
    {
        public string Cpf { get; set; }
        public List<Animal> Animal { get; set; } = new();
    }
}
