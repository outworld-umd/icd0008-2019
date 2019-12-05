using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL;
using GameEngine;
using Newtonsoft.Json;

namespace WebApp.Pages_Games
{
    public class CreateModel : PageModel
    {
        private readonly DAL.AppDbContext _context;

        public CreateModel(DAL.AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            SettingsState = await _context.Settings.FindAsync(1);
            return Page();
        }

        [BindProperty]
        public GameState GameState { get; set; }
        
        public SettingsState SettingsState { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {

            SettingsState = await _context.Settings.FindAsync(1);
            GameState.Data = JsonConvert.SerializeObject(new Game(SettingsState.BoardHeight, SettingsState.BoardWidth));
            _context.Games.Add(GameState);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Play", new {id = GameState.Name});
        }
    }
}
