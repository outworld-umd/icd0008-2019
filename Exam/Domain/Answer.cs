using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Answer
    {
        public int AnswerId { get; set; }

        public int PersonId { get; set; }
        public Person? Person { get; set; }

        public int ChoiceId { get; set; }
        public Choice? Choice { get; set; }
        
        public DateTime AnswerTime { get; set; }
    }
}