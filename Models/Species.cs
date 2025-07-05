using System.Security.Policy;

namespace system_petshop.Models
{
    public class Species
    {
        public int SpeciesId {  get; set; }
        public string Name {  get; set; }
        public List<Animal> Animal { get; set; } = new();
    }
}