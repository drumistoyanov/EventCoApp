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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EventCoApp.Common.Enums;
using EventCoApp.WebApp.Models.Extensions;

namespace EventCoApp.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
           UserManager<User> userManager, EventCoContext context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger) : base(userManager, context, hostEnvironment)
        {
            _logger = logger;

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
        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation("Log message in the Index() method");
            var listNews = new List<NewsListItemViewModel>();
            var news = _context.News
                .Include(n => n.CreatedBy)
                .Include(n=>n.Images)
                .Where(n => n.Approved == true);
            if (news.Any())
            {
                var log = await _context.Logs.FirstOrDefaultAsync(l => l.Type == LogType.SiteCounter);
                return View(new SearchEventsModel { Locations = GetLocationsList(), EventTypes = GetEventTypesList(), From = DateTime.Now, To = DateTime.Now, SiteVisitors = log.SiteCounter, News = news.ToList().ToListViewModel() });
            }
            else
            {
                var log = await _context.Logs.FirstOrDefaultAsync(l => l.Type == LogType.SiteCounter);

                return View(new SearchEventsModel { Locations = GetLocationsList(), EventTypes = GetEventTypesList(), From = DateTime.Now, To = DateTime.Now, SiteVisitors = log.SiteCounter });

            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message, string page)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message, Page = page }); ;
        }
    }
}
