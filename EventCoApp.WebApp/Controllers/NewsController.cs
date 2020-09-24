using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using NewsletterSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;
using System.IO;
using Microsoft.EntityFrameworkCore;
using EventCoApp.Common.Enums;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;

namespace EventCoApp.WebApp.Controllers
{
    public class NewsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        public NewsController(UserManager<User> userManager, EventCoContext context, ILogger<EventsController> logger, IWebHostEnvironment hostEnvironment) : base(userManager, context,hostEnvironment)
        {
            _logger = logger;
        }
        [HttpGet]
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
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            if (!HasPermission("CREATE_NEWS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewsViewModel model)
        {
            if (!HasPermission("CREATE_NEWS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (ModelState.IsValid)
            {
                int userId = GetCurrentUserId();

                model.CreatedById = userId;

                if (model.ImagesFiles != null || model.ImagesFiles.Count != 0)
                {
                    model.Images = new List<NewsImage>();
                    foreach (var formFile in model.ImagesFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            var file = UploadedFile(formFile);
                            var image = new NewsImageViewModel()
                            {
                                CreatedById = userId,
                                ImageData = file,
                                CreatedOn = DateTime.Now

                            };
                            model.Images.Add(image.ToEntity());
                        }
                    }
                    var news = model.ToEntity();

                    _context.News.Add(news);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
        private string UploadedFile(IFormFile image)
        {
            string uniqueFileName = null;

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                image.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        public async Task<IActionResult> Details(int id)
        {
            string userId = null;
            var existLog = false;
            var news = await _context.News
                .Include(e => e.CreatedBy)
                .Include(e => e.Images)
                .SingleAsync(e => e.ID == id);
            bool isAuthenticated = User.Identity.IsAuthenticated;

            if (news == null)
            {
                _logger.LogError("Error showing event details");
                return Error("Error showing event details", "Events"); ;
            }
            if (isAuthenticated)
            {
                userId = GetCurrentUserId().ToString();
                existLog = await _context.Logs.AnyAsync(l => l.UserId == userId && l.NewsId == id && l.Type == LogType.NewsCounter);
                if (!existLog)
                {
                    var log = new Log()
                    {
                        NewsId = id,
                        UserId = userId,
                        Type = LogType.NewsCounter,
                        CreatedOn = DateTime.Now
                    };
                    _context.Logs.Add(log);
                    news.Counter++;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                userId = Request.Cookies["VisitorId"];
                existLog = await _context.Logs.AnyAsync(l => l.UserId == userId && l.NewsId == id && l.Type == LogType.NewsCounter);
                if (!existLog)
                {
                    var log = new Log()
                    {
                        NewsId = id,
                        UserId = userId,
                        Type = LogType.NewsCounter,
                        CreatedOn = DateTime.Now
                    };
                    _context.Logs.Add(log);
                    news.Counter++;
                    await _context.SaveChangesAsync();
                }
            }

            return View(news.ToDetailsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> ApproveNews(int id)
        {
            if (!HasPermission("APPROVE_NEWS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }
            var news = await _context.News
                .Include(e => e.CreatedBy)
                .SingleAsync(e => e.ID == id);
            bool isAuthenticated = User.Identity.IsAuthenticated;


            if (news == null)
            {
                _logger.LogError("Error approving news");
                return Error("Error approving news", "News");
            }
            if (news.Approved == false)
            {
                news.Approved = true;
            }
            else
            {
                news.Approved = false;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message, string page)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message, Page = page }); ;
        }

    }
}
