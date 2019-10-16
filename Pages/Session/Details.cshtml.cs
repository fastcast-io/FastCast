﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastCast.Models;

namespace FastCast.Pages.Session
{
    public class DetailsModel : PageModel
    {
        private readonly FastCast.Models.FastCastContext _context;

        public DetailsModel(FastCast.Models.FastCastContext context)
        {
            _context = context;
        }

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
    }
}
