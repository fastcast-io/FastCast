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

        private readonly ILogger<IndexModel> _logger;

        private readonly IFastCastService _fastCastService;

        public IndexModel(ILogger<IndexModel> logger, FastCastContext context, IFastCastService fastCastService)
        {
            _context = context;
            _logger = logger;
            _fastCastService = fastCastService;
        }

        public void OnPost()
        {
            var authCode = Request.Form["AuthCode"];
            var latitude = Request.Form["Latitude"];
            var longitude = Request.Form["Longitude"];


            Debug.WriteLine($"Latitude: {latitude}, Longitude: {longitude}");
            try
            {
                var query = from session in _context.Session
                            where session.SessionCode == (String) authCode
                            select session;

                var selectedSession = query.FirstOrDefault<FastCast.Models.Session>();

                FastCastCoordinate sessionCoord = new FastCastCoordinate(latitude: selectedSession.Latitude, longitude: selectedSession.Longitude);
                FastCastCoordinate userCoord = new FastCastCoordinate(latitude: Double.Parse(latitude), longitude: Double.Parse(longitude));
                Double difference = sessionCoord.GetDistanceTo(userCoord); // Distance is in meters

                // TEST: AROUND A CLASS/OFFICE IN THE CS DEPT:
                // LAT: 33.587446
                // LONG: -101.875457 <- session can be saved with it

                // AROUND MY HOUSE: 
                // LAT: 33.588424
                // LON: -101.858219


                // WITHIN RANGE
                // LAT: 33.588426
                // LONG: -101.858248
                // LESS THAN 30 meters

                // SLIGHTLY OUT OF RANGE
                // LAT: 35.588451
                // LONG:-101.858705 
                // => AROUND 45.90 m
                // TODO: IMPLEMENT INPUT VALIDATION FOR THE LATITUDE AND LONGITUDE
                Debug.WriteLine($"********************************************\n\n\nDIFFERENCE IS {difference}\n\n\n********************************************\n\n\n");
                if (difference > selectedSession.Radius) 
                {
                    var ex = new Exception("");
                    ex.Data.Add("LOCATION ERROR", "Your longitude and latitude is not in the region specified by the initiator. Get closer");
                    throw ex;
                }
                ViewData["Error"] = null;


                //_fastCastService.AddData("SessionName", selectedSession.SessionName);
                _fastCastService.AddData("SessionCode", selectedSession.SessionCode);
                _fastCastService.AddData("SessionFormId", selectedSession.FormId);
                //Response.Redirect("/Answer");

            } catch (Exception e)
            {
                ViewData["Error"] = "We could not find any session linked to your session code :(";
               
                if(e.Data.Contains("LOCATION ERROR"))
                {
                    ViewData["Error"] = $"\n{e.Data["LOCATION ERROR"]} :(";
                }
            }
        }
    }
}
