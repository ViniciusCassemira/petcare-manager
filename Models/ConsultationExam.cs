namespace system_petshop.Models
{
    public class ConsultationExam
    {
        public int ConsultationExamId { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public Consultation Consultation { get; set; }
        public int ConsultationId { get; set; }
        public Exam Exam { get; set; }
        public int ExamId { get; set; }
    }
}
