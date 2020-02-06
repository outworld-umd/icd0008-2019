using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using Domain;
using Type = Domain.Type;

namespace ExamApp.Pages_Quizzes
{
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;
        
        public string? Search { get; set; }

        public Type Type { get; set; }

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Quiz> Quiz { get;set; } = new List<Quiz>();

        public string PersonName { get; set; } = default!;
        public int? PersonId { get; set; }

        public async Task OnGetAsync(int? personId, string? search, string? toDoActionReset)
        {
            if (toDoActionReset == "Reset") Search = "";
            else
            {
                if (!string.IsNullOrWhiteSpace(search)) Search = search.ToLower().Trim();
            }
            if (personId == null) return;
            var person = _context.Persons.FirstOrDefault(x => x.PersonId == personId);
            PersonName = person?.Name ?? "";
            PersonId = person?.PersonId;
            if (string.IsNullOrWhiteSpace(Search))
            {
                Quiz = await _context.Quizzes
                    .Include(x => x.Questions)
                    .ThenInclude(x => x.Choices).ToListAsync();
            }
            else
            {
                Quiz = await _context.Quizzes
                    .Include(x => x.Questions)
                    .ThenInclude(x => x.Choices)
                    .Where(x => x.Name.ToLower().Contains(Search) ||
                                                         x.Description.ToLower().Contains(Search)).ToListAsync();
                
            }
            
        }
    }
}
