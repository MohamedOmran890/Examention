namespace Examention.Data.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Exam>? Exams { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}