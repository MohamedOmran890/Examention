using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examention.Data.Models
{

    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public ICollection<ExamStudent> ExamStudent { get; set; }
    }
}
