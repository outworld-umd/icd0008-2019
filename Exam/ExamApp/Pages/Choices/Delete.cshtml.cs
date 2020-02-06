using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;

namespace ExamApp.Pages_Choices
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Choice Choice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int? redirect)
        {
            if (id == null)
            {
                return NotFound();
            }

            Choice = await _context.Choices.FindAsync(id);

            if (Choice != null)
            {
                var question = _context.Questions.FirstOrDefault(x => x.CorrectChoiceId == Choice.ChoiceId);
                if (question != null)
                {
                    question.CorrectChoiceId = null;
                    _context.Questions.Update(question);  
                }
                _context.Choices.Remove(Choice);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Choices/Create", new {id = redirect});
        }
    }
}
