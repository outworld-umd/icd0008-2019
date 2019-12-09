using System.Linq;
using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages_Games {

    public class CreateModel : PageModel {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context) {
            _context = context;
        }

        [BindProperty] public GameState GameState { get; set; }

        public SettingsState SettingsState { get; set; }

        public async Task<IActionResult> OnGetAsync() {
            SettingsState = await _context.Settings.FindAsync(1);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            SettingsState = await _context.Settings.FindAsync(1);
            GameState.Data = JsonConvert.SerializeObject(new Game(SettingsState.BoardHeight, SettingsState.BoardWidth));
            if (_context.Games.Any(g => g.Name == GameState.Name))
                _context.Games.Update(GameState);
            else _context.Games.Add(GameState);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Play", new {id = GameState.Name});
        }
    }

}