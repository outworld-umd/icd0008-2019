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

namespace ExamApp.Pages_Choices
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "QuestionId", "Title");
            QuestionID = id;
            QuizID = _context.Questions.FirstOrDefault(x => x.QuestionId == id)?.QuizId;
            Choices = _context.Choices.Where(x => x.QuestionId == id).ToList();
            return Page();
        }

        [BindProperty]
        public Choice Choice { get; set; } = default!;
        
        public int? QuestionID { get; set; }
        
        public int? QuizID { get; set; }
        
        public ICollection<Choice> Choices { get; set; } = default!;

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Choices.Add(Choice);
            await _context.SaveChangesAsync();

            return RedirectToPage(new {id = Choice.QuestionId});
        }
    }
}
