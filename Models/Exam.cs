namespace system_petshop.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public List<ConsultationExam> ConsultationExams { get; set; } = new();
    }
}
