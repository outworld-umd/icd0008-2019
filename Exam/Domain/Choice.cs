using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Choice
    {
        public int ChoiceId { get; set; }

        public string Text { get; set; } = default!;
        
        public int QuestionId { get; set; }
        [InverseProperty("Choices")]
        public Question? Question { get; set; }

        public ICollection<Answer>? Answers { get; set; }
    }
}