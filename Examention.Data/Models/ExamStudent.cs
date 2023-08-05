namespace Examention.Data.Models
{
    public class ExamStudent
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public double Grade { get; set; }
    }
}