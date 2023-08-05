using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.Data.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<Question> Questions { get; set; }=new List<Question>();
        public virtual ICollection<ExamStudent>? ExamStudents { get; set; }=new List<ExamStudent>();
        public int DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
