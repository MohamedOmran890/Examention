namespace Examention.Api.DTO
{
    public class ExamGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }
        public int LevelId { get; set; }
        public int DoctorId { get; set; }
    }
}
