using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FastCast.Models;
using Microsoft.EntityFrameworkCore;


namespace FastCast.Pages.Session
{
    public class StartModel : PageModel
    {

        private readonly FastCastContext _context;

        public StartModel(FastCastContext context)
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

        //public void OnGet()
        //{

        //}
    }
}