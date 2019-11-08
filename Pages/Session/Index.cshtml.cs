﻿using System;
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

        public void StartSession(int sessionTime, int sessionId)
        {
            var timer = new Timer(sessionTime);

            timer.Elapsed += (sender, e) => TimerEnded(sender, e, sessionId);
            timer.AutoReset = false;
            timer.Start();
        }

        public void TimerEnded(object sender, ElapsedEventArgs e, int sessionId)
        {

        }

        public void OnPostSessionInput(int sessionTime, int sessionId)
        {
            Console.WriteLine($"Called with {sessionTime} {sessionId}");
            StartSession(sessionTime, sessionId);
        }

    }
}
