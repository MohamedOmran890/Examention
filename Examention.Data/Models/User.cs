using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Examention.Data.Models
{
    [Table("Users")]
    public class User:IdentityUser
    {
        public int FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }
}