using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using Domain;

namespace ExamApp.Pages_Quizzes
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? personId)
        {
            PersonId = personId;
            return Page();
        }

        public int? PersonId { get; set; }

        [BindProperty]
        public Quiz Quiz { get; set; } = default!;

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? personId)
        {
            if (personId == null) return Page();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Quiz.CreationTime = DateTime.Now;

            _context.Quizzes.Add(Quiz);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new {personId});
        }
    }
}
