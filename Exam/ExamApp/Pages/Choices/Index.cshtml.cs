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
    public class IndexModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public IList<Choice> Choice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Choice = await _context.Choices
                .Include(c => c.Question).ToListAsync();
        }
    }
}
