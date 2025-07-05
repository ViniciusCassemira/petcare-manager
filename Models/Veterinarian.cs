using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;

namespace system_petshop.Models
{
    public class Veterinarian : User
    {
        public string Cfmv { get; set; }

        [ValidateNever]
        public Specialty Specialty { get; set; }
        public int SpecialtyId { get; set; }
        public List<Consultation> Consultation { get; set; } = new List<Consultation>();
    }
}