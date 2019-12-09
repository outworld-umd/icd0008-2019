using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace WebApp.Pages_Settings {

    public class EditModel : PageModel {
        private readonly DAL.AppDbContext _context;

        public EditModel(DAL.AppDbContext context) {
            _context = context;
        }

        [BindProperty] public SettingsState SettingsState { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id) {
            if (id == null) {
                return NotFound();
            }

            SettingsState = await _context.Settings.FirstOrDefaultAsync(m => m.SettingsId == id);

            if (SettingsState == null) {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync() {
            if (Request.Form["default"].Equals("Set Defaults"))
                SettingsState = new SettingsState {BoardHeight = 6, BoardWidth = 7};

            if (!ModelState.IsValid) {
                return Page();
            }

            _context.Attach(SettingsState).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!SettingsStateExists(SettingsState.SettingsId)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return RedirectToPage("./Current");
        }

        private bool SettingsStateExists(int id) {
            return _context.Settings.Any(e => e.SettingsId == id);
        }
    }

}