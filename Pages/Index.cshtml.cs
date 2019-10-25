using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FastCast.Models;
using Microsoft.EntityFrameworkCore;

namespace FastCast.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FastCastContext _context;

        public IList<Models.Session> Session { get;set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, FastCastContext context)
        {
            _context = context;
            _logger = logger;
        }

        public void OnPost()
        {
            var authCode = Request.Form["AuthCode"];

            Session = _context.Session.ToList();

            try
            {
                var selectedSession = (from s in Session
                                       where s.SessionCode == authCode
                                       select s).Single();

                ViewData["FormId"] = selectedSession.FormId;
                ViewData["SessionStatus"] = "OK";
            } catch (Exception)
            {
                ViewData["SessionStatus"] = "BAD";
            }



            // Get the session with this session ID using Linq
            // Set the ViewData form ID so it displays
            // Set the ViewData session timer so it displays

            // Use this if we want to display it on the front page still
        }
    }
}
