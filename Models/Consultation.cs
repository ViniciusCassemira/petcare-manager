using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace system_petshop.Models
{
    public class Consultation
    {
        public int ConsultationId { get; set; }
        public DateTime ConsultationDate { get; set; }
        public string Note { get; set; }

        [ValidateNever]
        public Animal Animal { get; set; }

        public int AnimalId { get; set; }

        [ValidateNever]
        public Veterinarian Veterinarian { get; set; }
        
        public int VeterinarianId { get; set; }

        public List<ConsultationMedicine> ConsultationMedicines { get; set; } = new();
        public List<ConsultationExam> ConsultationExams { get; set; } = new();
    }
}
