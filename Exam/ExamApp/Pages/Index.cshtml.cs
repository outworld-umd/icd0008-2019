using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExamApp.Pages
{
    public class Index : PageModel
    {
        [BindProperty] public string PersonName { get; set; } = default!;
        
        private readonly DAL.AppDbContext _context;

        public Index(DAL.AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync(string? personName)
        {
            if (personName == null) return Page();
            
            PersonName = personName;

            
            var found = _context.Persons.Any(x => x.Name == PersonName);
            if (!found)
            {
                var person = new Person();
                person.Name = PersonName;
                _context.Persons.Add(person);
                await _context.SaveChangesAsync();
            }

            var foundPerson = _context.Persons.FirstOrDefault(x => x.Name == PersonName);
            if (foundPerson != null)
            {
                return RedirectToPage("/Quizzes/Index", new { personId = foundPerson.PersonId});
            }

            return Page();
        }
        
    }
}