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
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Microsoft.AspNetCore.Http;

namespace EventCoApp.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
           UserManager<User> userManager, EventCoContext context, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger ) : base(userManager, context, hostEnvironment)
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
        public IActionResult Index()
        {
            _logger.LogInformation("Log message in the Index() method");
            var listNews = new List<NewsViewModel>();
            var newsApiClient = new NewsApiClient("1a8cf277ffe84b368b0b8dc0f011e804");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Culture",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.Date
            });
            if (articlesResponse.Status == Statuses.Ok)
            {

                // here's the first 20
                foreach (var article in articlesResponse.Articles)
                {
                    var newArticle = new NewsViewModel()
                    {
                        Author = article.Author,
                        PublishedAt = article.PublishedAt,
                        Description = article.Description,
                        Title = article.Title,
                        Url = article.Url,
                        UrlToImg = article.UrlToImage,

                    };
                    listNews.Add(newArticle);
                }
            }

            return View(new SearchEventsModel { Locations = GetLocationsList(), EventTypes = GetEventTypesList(), From = DateTime.Now, To = DateTime.Now, News = listNews });
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
