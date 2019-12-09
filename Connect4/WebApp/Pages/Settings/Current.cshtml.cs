using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages_Settings {

    public class IndexModel : PageModel {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context) {
            _context = context;
        }

        public IList<SettingsState> SettingsState { get; set; }

        public async Task OnGetAsync() {
            SettingsState = await _context.Settings.ToListAsync();
        }
    }

}