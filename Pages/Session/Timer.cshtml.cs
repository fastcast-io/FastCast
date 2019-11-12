using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FastCast.Pages.Session
{
    public class TimerModel : PageModel
    {
        private readonly FastCast.Models.FastCastContext _context;

        public TimerModel(FastCast.Models.FastCastContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Session Session { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session.FirstOrDefaultAsync(m => m.SessionId == id);

            if (Session == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Session = await _context.Session.FindAsync(id);

            if (Session != null)
            {
                Session.IsLive = true;
                _context.Session.Update(Session);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");

        }
    }
}
