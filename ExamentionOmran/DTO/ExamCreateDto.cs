using Examention.Data.Models;

namespace Examention.Api.DTO
{
    public class ExamCreateDto
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public int DoctorId { get; set; }
        //public int LevelId { get; set; }
        //public Level Level { get; set; }

    }
}
