using Examention.Data.Models;

namespace Examention.Api.DTO
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ChoiceId { get; set; }
        public ICollection<Choice> Choices { get; set; }
        public int CorrectChoiceId { get; set; }
    }
}
