using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace ExamApp.Pages_Quizzes
{
    public class DetailsModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DetailsModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public Quiz Quiz { get; set; } = default!;

        public ICollection<Answer> Answers { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Answers = await _context.Answers.Include(x => x.Choice)
                .ThenInclude(x => x!.Question)
                .ThenInclude(x => x!.Quiz)
                .Where(x => x.Choice!.Question!.QuizId == id).ToListAsync();
            
            Quiz = await _context.Quizzes
                .Include(x => x.Questions)
                .ThenInclude(x => x.Choices).FirstOrDefaultAsync(m => m.QuizId == id);
            Quiz.Questions = _context.Questions.Where(x => x.QuizId == id).ToList();

            if (Quiz == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
