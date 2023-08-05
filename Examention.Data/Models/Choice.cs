using System.Text.Json.Serialization;

namespace Examention.Data.Models
{
    public class Choice
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        [JsonIgnore]
        public virtual Question Question { get; set; }
    }
}