using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastCast.Models;

namespace FastCast.Pages.Session
{
    public class IndexModel : PageModel
    {
        private readonly FastCast.Models.FastCastContext _context;

        public IndexModel(FastCast.Models.FastCastContext context)
        {
            _context = context;
        }

        public IList<Models.Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await _context.Session.ToListAsync();
        }
    }
}
