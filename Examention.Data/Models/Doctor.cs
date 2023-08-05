using System.ComponentModel.DataAnnotations.Schema;

namespace Examention.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}