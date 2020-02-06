using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace ExamApp.Pages_Questions
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public List<Question> Question { get; set; } = new List<Question>();
        public List<Choice> Choices { get; set; } = new List<Choice>();

        [BindProperty]
        public List<int> ChosenIds { get; set; }
        
        public Quiz Quiz { get; set; }
        public int? PersonId { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, int? personId)
        {
            
            if (personId == null) NotFound();
            if (id == null) NotFound();
            PersonId = personId;
            Quiz = _context.Quizzes.FirstOrDefault(x => x.QuizId == id);
            Question = await _context.Questions
                .Include(q => q.CorrectChoice)
                .Include(q => q.Quiz)
                .Include(x => x.Choices)
                .Where(x => x.QuizId == id).ToListAsync();
            //Choices = await _context.Choices.Include(x => x.Question).ToListAsync();
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? personId)
        {
            if (personId == null) return Page();
            if (ChosenIds.Count == 0) return Page();
            foreach (var chosenId in ChosenIds)
            {
                var answer = new Answer();
                answer.AnswerTime = DateTime.Now;
                answer.ChoiceId = chosenId;
                answer.PersonId = (int)personId;
                _context.Answers.Add(answer);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToPage("/Quizzes/Index", new {personId = personId});
        }

    }
}