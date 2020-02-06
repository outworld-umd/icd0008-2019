using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Pages_Questions
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public int? QuizID { get; set; } = null;
        
        public ICollection<Question> QuizQuestions { get; set; } = default!;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            QuizID = id;
            ViewData["CorrectChoiceId"] = new SelectList(_context.Choices, "ChoiceId", "Text");
            ViewData["QuizId"] = new SelectList(_context.Quizzes, "QuizId", "Name");
            QuizQuestions = _context.Questions.Where(x => x.QuizId == QuizID)
                .Include(q => q.CorrectChoice)
                .Include(q => q.Quiz).ToList();
            return Page();
        }

        [BindProperty]
        public Question Question { get; set; } = default!;

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Questions.Add(Question);
            await _context.SaveChangesAsync();

            return RedirectToPage(new {id = Question.QuizId});
        }
    }
}
