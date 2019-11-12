using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FastCast.Models;
using System.Timers;

namespace FastCast.Pages.Session
{
    public class IndexModel : PageModel
    {
        private readonly FastCastContext _context;

        public IndexModel(FastCastContext context)
        {
            _context = context;
        }

        public IList<Models.Session> Session { get;set; }

        public async Task OnGetAsync()
        {
            Session = await _context.Session.ToListAsync(); 
        }

        public void StartSession(int sessionTime, Guid sessionId)
        {
            var timer = new Timer(sessionTime);

            var sessionToUpdate = _context.Session
                    .Single(s => s.SessionId == sessionId);

            sessionToUpdate.IsLive = true;

            _context.Session.Update(sessionToUpdate);
            _context.SaveChanges();

            timer.Elapsed += (sender, e) => TimerEnded(sender, e, sessionId);
            timer.AutoReset = false;
            timer.Start();
        }

        public void TimerEnded(object sender, ElapsedEventArgs e, Guid sessionId)
        {
            ViewData["timerStarted"] = false;
        }

        public IActionResult OnPostSessionInput(int sessionTime, Guid sessionId)
        {
            StartSession(sessionTime, sessionId);
            ViewData["timerStarted"] = true;

            return RedirectToPage("./Index");
        }

    }
}
