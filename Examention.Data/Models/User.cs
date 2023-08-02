using Microsoft.AspNetCore.Identity;

namespace Examention.Data.Models
{
    public class User:IdentityUser
    {
        public int FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }
}