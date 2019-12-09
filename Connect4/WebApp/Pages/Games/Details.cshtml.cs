using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApp.Pages_Games {

    public class DetailsModel : PageModel {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context) {
            _context = context;
        }

        public GameState GameState { get; set; }
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(string id) {
            if (id == null) return NotFound();
            GameState = await _context.Games.FirstOrDefaultAsync(m => m.Name == id);
            if (GameState == null) return NotFound();
            Game = JsonConvert.DeserializeObject<Game>(GameState.Data);
            return Page();
        }
    }

}