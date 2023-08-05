namespace Examention.Data.Models
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
    }
}