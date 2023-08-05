using Examention.Data.Models;

namespace Examention.Api.DTO
{
    public class QuestionCreateDto
    {
        public string Text { get; set; }
        public int ExamId { get; set; }
        public IEnumerable<ChoiceCreateDto> Choices { get; set; }
        public int CorrectChoiceId { get; set; }
    }
}
