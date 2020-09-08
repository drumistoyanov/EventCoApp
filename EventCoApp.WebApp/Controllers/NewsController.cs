using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using NewsletterSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace EventCoApp.WebApp.Controllers
{
    public class NewsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        public NewsController(UserManager<User> userManager, EventCoContext context, ILogger<EventsController> logger) : base(userManager, context)
        {
            _logger =logger;
        }

        public IActionResult Index()
        {
            //var newsApiClient = new NewsApiClient("1a8cf277ffe84b368b0b8dc0f011e804");
            //var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            //{
            //    Q = "Apple",
            //    SortBy = SortBys.Popularity,
            //    Language = Languages.EN,
            //    From = new DateTime(2018, 1, 25)
            //});
            //if (articlesResponse.Status == Statuses.Ok)
            //{
            //    // total results found
            //    Console.WriteLine(articlesResponse.TotalResults);
            //    // here's the first 20
            //    foreach (var article in articlesResponse.Articles)
            //    {
            //        // title
            //        Console.WriteLine(article.Title);
            //        // author
            //        Console.WriteLine(article.Author);
            //        // description
            //        Console.WriteLine(article.Description);
            //        // url
            //        Console.WriteLine(article.Url);
            //        // published at
            //        Console.WriteLine(article.PublishedAt);
            //    }
            //}
            //Console.ReadLine();

            return View();
        }
    }
}
