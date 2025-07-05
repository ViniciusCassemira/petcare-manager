using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace system_petshop.Models
{
    public class Animal
    {
        public int AnimalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly DateBirth { get; set; }
        public int BreedId { get; set; }
        public int SpeciesId { get; set; }
        public int ClientId { get; set; }

        [ValidateNever]
        public Breed Breed { get; set; }

        [ValidateNever]
        public Species Species { get; set; }

        [ValidateNever]
        public Client Client { get; set; }

        public List<Consultation> Consultation { get; set; } = [];
    }
}