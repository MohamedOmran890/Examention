using Examention.Data.Models;

namespace Examention.Api.DTO
{
    public class QuestionGetDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public IEnumerable<Choice> Choices { get; set; }
        public int CorrectChoiceId { get; set; }
        public int ExamId { get; set; }
    }
}
