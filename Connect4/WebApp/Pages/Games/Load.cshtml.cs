using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL;
using GameEngine;
using Newtonsoft.Json;

namespace WebApp.Pages_Games {

    public class IndexModel : PageModel {
        private readonly DAL.AppDbContext _context;

        public IndexModel(DAL.AppDbContext context) {
            _context = context;
        }

        public IList<GameState> GameState { get; set; }

        public async Task OnGetAsync() {
            GameState = await _context.Games.ToListAsync();
            GameState.ToList().ForEach(g => {
                var game = JsonConvert.DeserializeObject<Game>(g.Data);
                g.Data = $"Field size: {game.Height}x{game.Width}";
            });
        }
    }

}