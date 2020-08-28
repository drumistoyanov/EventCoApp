using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EventCoApp.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using EventCoApp.DataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.Hosting;
using EventCoApp.DataAccessLibrary.Models;

namespace EventCoApp.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
           UserManager<User> userManager, EventCoContext context, IWebHostEnvironment hostEnvironment) : base(userManager, context, hostEnvironment)
        {
        }
        private IEnumerable<SelectListItem> GetLocationsList()
        {
            var locations = new List<SelectListItem>();
            var location = new SelectListItem { Text = "All", Value = 9999.ToString() };
            locations.Add(location);
            foreach (var s in _context.Locations.ToList())
            {
                location = new SelectListItem { Text = s.Name, Value = s.Id.ToString() };
                locations.Add(location);
            }
            return locations;
        }
        private IEnumerable<SelectListItem> GetEventTypesList()
        {
            var eventTypes = new List<SelectListItem>();
            foreach (var s in _context.EventTypes.ToList())
            {
                var eventt = new SelectListItem { Text = s.Name, Value = s.Id.ToString() };
                eventTypes.Add(eventt);
            }
            return eventTypes;
        }
        public IActionResult Index()
        {

            return View(new SearchEventsModel { Locations = GetLocationsList(), EventTypes = GetEventTypesList(), From = DateTime.Now, To = DateTime.Now });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
