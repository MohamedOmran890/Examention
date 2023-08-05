using System.Text.Json.Serialization;

namespace Examention.Data.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }
        public int CorrectChoiceId { get; set; }
        public int ExamId { get; set; }
        [JsonIgnore]
        public virtual Exam Exam { get; set; }
    }
}