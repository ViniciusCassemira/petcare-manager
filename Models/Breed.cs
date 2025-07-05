namespace system_petshop.Models
{
    public class Breed
    {
        public int BreedId {  get; set; }
        public string Name { get; set; }
        public List<Animal> Animal { get; set; } = new();
    }
}
