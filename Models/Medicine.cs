namespace system_petshop.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Description {  get; set; }
        public decimal Value { get; set; }
        public List<ConsultationMedicine> ConsultationMedicines { get; set; } = new();
    }
}
