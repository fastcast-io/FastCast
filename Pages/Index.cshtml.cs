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
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

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
            var latitude = Request.Form["Latitude"];
            var longitude = Request.Form["Longitude"];

            Session = _context.Session.ToList();

            Debug.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
            try
            {
                var selectedSession = (from s in Session
                                       where s.SessionCode == authCode
                                       select s).Single();

                

                ViewData["FormId"] = selectedSession.FormId;
                ViewData["IsLIve"] = selectedSession.IsLive;
                ViewData["SessionStatus"] = true;
            } catch (Exception)
            {
                ViewData["SessionStatus"] = false;
            }
        }
    }
}
