using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using FastCast.Models;

namespace FastCast.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            var authCode = Request.Form["AuthCode"];

            var sessions = new List<Models.Session>
            {
                new Models.Session
                {
                    SessionCode = "project",
                    FormId = "1FAIpQLSchTqjGMem0vIguYs3aNbFV-F7PPLV6eIAUrWu_KZG8kqMfDA"
                }
            };

            try
            {
                var selectedSession = (from s in sessions
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
