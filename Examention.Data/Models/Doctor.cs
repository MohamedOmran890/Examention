using System.ComponentModel.DataAnnotations.Schema;

namespace Examention.Data.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}