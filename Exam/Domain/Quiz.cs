using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Quiz
    {
        public int QuizId { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;
        
        public Type Type { get; set; }

        public DateTime CreationTime { get; set; }

        public ICollection<Question>? Questions { get; set; }
        
    }

}