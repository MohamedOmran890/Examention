using System.ComponentModel.DataAnnotations;

namespace Examention.Api.DTO
{
    public class RegisterStudentDto:RegisterDto
    {
        public int LevelId { get; set; }

    }
}
