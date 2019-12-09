using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace WebApp.Pages_Games {

    public class DeleteModel : PageModel {
        private readonly DAL.AppDbContext _context;

        public DeleteModel(DAL.AppDbContext context) {
            _context = context;
        }

        [BindProperty] public GameState GameState { get; set; }

        public async Task<IActionResult> OnGetAsync(string id) {
            if (id == null) {
                return NotFound();
            }

            GameState = await _context.Games.FirstOrDefaultAsync(m => m.Name == id);

            if (GameState == null) {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id) {
            if (id == null) {
                return NotFound();
            }

            GameState = await _context.Games.FindAsync(id);

            if (GameState != null) {
                _context.Games.Remove(GameState);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Load");
        }
    }

}