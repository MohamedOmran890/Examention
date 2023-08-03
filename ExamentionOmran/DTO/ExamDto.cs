using Examention.Data.Models;

namespace Examention.Api.DTO
{
    public class ExamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}
