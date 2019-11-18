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
    public class UserQuery
    {
        public string AuthCode { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }

        //public UserQuery(string authCode, double latitude, double longitude)
        //{
        //    this.authCode = authCode;
        //    this.latitude = latitude;
        //    this.longitude = longitude;
        //}

        //public string AuthCode { get => authCode; set => authCode = value; }
        //public double Latitude { get => latitude; set => latitude = value; }
        //public double Longitude { get => longitude; set => longitude = value; }
    }

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

        //public void //OnPost([FromBody] string AuthCode, [FromBody] Double Latitude, [FromBody] Double Longitude)
        public async Task OnPost(UserQuery userQuery)
        {
            //Debug.WriteLine($"{Request.Form["AuthCode"]} | {Request.Form["Latitude"]} | {Request.Form["Longitude"]}");
            //var AuthCode = Request.Form["AuthCode"].ToString();
            //var Latitude = Request.Form["Latitude"].ToString().Length > 0 ? Double.Parse(Request.Form["Latitude"]) : 0;
            //var Longitude = Request.Form["Latitude"].ToString().Length > 0 ? Double.Parse(Request.Form["Longitude"]) : 0;
            //Debug.WriteLine("YOO I WAS CALLED JUNIOR GOOD JOB!");
            // Debug.WriteLine($"Authcode: {element.AuthCode}");
            // Debug.WriteLine($"Latitude: {element.Latitude}");
            // Debug.WriteLine($"Longitude: {element.Longitude}");
            //var authCode = Request.Form["AuthCode"];
            //var latitude = Request.Form["Latitude"];
            //var longitude = Request.Form["Longitude"];
            var AuthCode = userQuery.AuthCode;
            var Latitude = userQuery.Latitude;
            var Longitude = userQuery.Longitude;


            Debug.WriteLine($"Latitude: {Latitude}, Longitude: {Longitude}");
            try
            {
                if (Latitude == 0 || Longitude == 0)
                {
                    var ex = new Exception("");
                    ex.Data.Add("NO LOCATION",
                        "We could not get your location sorry. Please reload the page or resubmit");
                    throw ex;
                }

                var query = from session in _context.Session
                            where session.SessionCode == AuthCode
                            select session;

                var selectedSession = query.FirstOrDefault<FastCast.Models.Session>();

                FastCastCoordinate sessionCoord = new FastCastCoordinate(latitude: selectedSession.Latitude,
                                                                         longitude: selectedSession.Longitude);

                FastCastCoordinate userCoord = new FastCastCoordinate(latitude: Latitude,
                                                                      longitude: Longitude);

                Double difference = sessionCoord.GetDistanceTo(userCoord); // Distance is in meters

                Debug.WriteLine(
                    $"********************************************" +
                    $"\n\n\n ${DateTime.Now.ToString()} DIFFERENCE IS {difference}" +
                    $"\n\n\n********************************************\n\n\n"
                    );

                if (difference > selectedSession.Radius)
                {
                    var ex = new Exception("");
                    ex.Data.Add("LOCATION ERROR",
                        "Your longitude and latitude is not in the region specified by the initiator. Get closer");
                    throw ex;
                }
                ViewData["Error"] = null;


                _fastCastService.AddData("SessionName", selectedSession.SessionName, true);
                _fastCastService.AddData("SessionCode", selectedSession.SessionCode, true);
                _fastCastService.AddData("SessionDuration", selectedSession.Timer.ToString(), true);
                _fastCastService.AddData("SessionFormId", selectedSession.FormId, true);
                //Response.Redirect("/Answer");

            }
            catch (Exception e)
            {
                if (e is System.NullReferenceException)
                {
                    ViewData["Error"] = "We could not find any session linked to your session code :(.";
                }
                else
                {
                    if (e.Data.Contains("LOCATION ERROR"))
                    {
                        ViewData["Error"] = e.Data["LOCATION ERROR"];
                    }
                    else if (e.Data.Contains("NO LOCATION"))
                    {
                        ViewData["Error"] = e.Data["NO LOCATION"];
                    }
                    else
                    {
                        ViewData["Error"] = "There was an error. Please contact us with this error: " + e.Message;
                    }
                }


            }

        }
    }
}

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
