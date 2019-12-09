using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using GameEngine;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace WebApp.Pages_Games
{
    public class EditModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public GameState GameState { get; set; }
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            GameState = await _context.Games.FirstOrDefaultAsync(m => m.Name == id);

            if (GameState == null)
            {
                return NotFound();
            }

            Game = JsonConvert.DeserializeObject<Game>(GameState.Data);
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            GameState = await _context.Games.FirstOrDefaultAsync(m => m.Name == Request.Form["game"].ToString());
            Game = JsonConvert.DeserializeObject<Game>(GameState.Data);
            if (Game.DropDisc(Request.Form["move"] == "computer" ? Game.GetColumn() : int.Parse(Request.Form["move"]))) {
                if (Game.CheckWinner()) GameState.Winner = Game.FirstPlayerWinner ? 1 : 2;
                if (Game.CheckGameEnd()) GameState.Winner = 3;
            }

            GameState.Data = JsonConvert.SerializeObject(Game);
            _context.Games.Update(GameState);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!GameStateExists(GameState.Name))
                {
                    return NotFound();
                }
                throw;
            }

            Console.WriteLine(Request.Form["move"]);
            return RedirectToPage("./Play", new {id = Request.Form["game"]});
        }

        private bool GameStateExists(string id)
        {
            return _context.Games.Any(e => e.Name == id);
        }
    }
}
