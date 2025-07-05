namespace system_petshop.Models
{
    public class ConsultationMedicine
    {
        public int ConsultationMedicineId { get; set; }
        
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public Consultation Consultation { get; set; }
        public int ConsultationId { get; set; }
        public Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
    }
}
