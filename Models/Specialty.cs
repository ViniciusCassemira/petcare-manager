namespace system_petshop.Models
{
    public class Specialty
    {
        public int SpecialtyId { get; set; }
        public string Description { get; set; }
        public List<Veterinarian> Veterinarian { get; set; } = new List<Veterinarian>();
    }
}