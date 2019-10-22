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
            var sessionId = Request.Form["SessionId"];
            
            // Get the session with this session ID using Linq
            // Set the ViewData form ID so it displays
            // Set the ViewData session timer so it displays

            // Use this if we want to display it on the front page still
            ViewData["SessionId"] = Request.Form["SessionId"];
        }
    }
}
