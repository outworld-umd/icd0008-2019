using System.Linq;
using System.Threading.Tasks;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebApp.Pages_Games {

    public class EditModel : PageModel {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context) {
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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            GameState = await _context.Games.FirstOrDefaultAsync(m => m.Name == Request.Form["game"].ToString());
            Game = JsonConvert.DeserializeObject<Game>(GameState.Data);
            if (Game.DropDisc(int.Parse(Request.Form["move"]))) {
                GameState.Winner = Game.CheckGameStatus();
                if (GameState.Opponent == 1 && GameState.Winner == 0 && Game.DropDisc(Game.GetColumn()))
                    GameState.Winner = Game.CheckGameStatus();
            }
            GameState.Data = JsonConvert.SerializeObject(Game);
            _context.Games.Update(GameState);
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!GameStateExists(GameState.Name)) return NotFound();
                throw;
            }
            return RedirectToPage(GameState.Winner == 0 ? "./Play" : "./Details", new {id = Request.Form["game"]});
        }

        private bool GameStateExists(string id) {
            return _context.Games.Any(e => e.Name == id);
        }
    }

}