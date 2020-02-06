using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Domain
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Title { get; set; } = default!;
        
        public string? Description { get; set; }
        
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        public int? CorrectChoiceId { get; set; }
        [ForeignKey(nameof(CorrectChoiceId))]
        public Choice? CorrectChoice { get; set; }
        
        public ICollection<Choice>? Choices { get; set; }
    }
}