﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FastCast.Models;

namespace FastCast.Pages.Session
{
    public class CreateModel : PageModel
    {
        private readonly FastCast.Models.FastCastContext _context;

        public CreateModel(FastCast.Models.FastCastContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Session Session { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (SessionCodeExists(Session.SessionCode))
            {
                ViewData["codeExists"] = true;
                return Page();
            }
            else
            {
                ViewData["codeExists"] = false;
                Session.IsLive = false;

                _context.Session.Add(Session);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }

        }

        public bool SessionCodeExists(string sessionCode)
        {
            var session = _context.Session.ToList();

            try
            {
                var selectedSession = (from s in session
                                       where s.SessionCode == sessionCode
                                       select s).Single();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }
    }
}
