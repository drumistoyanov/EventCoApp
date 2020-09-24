using EventCoApp.Common.Enums;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Controllers
{
    public class EventsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        public EventsController(
           UserManager<User> userManager, EventCoContext context, IWebHostEnvironment hostEnvironment, ILogger<EventsController> logger) : base(userManager, context, hostEnvironment)
        {
            _logger = logger;
        }
        public IActionResult Index(int? page)
        {
            _logger.LogInformation("Opened Index() page");
            var pageNumber = page ?? 1;

            var pageSize = 6;

            var events = _context.Events
                .Include(e => e.CreatedBy)
                .Include(e => e.Images)
                .Include(e => e.EventType)
                .Include(e => e.Location)
                .Include(e => e.Tickets)
                .OrderByDescending(e => e.Counter)
                .ThenByDescending(e => e.When);

            foreach (var item in events)
            {
                if (item.When <= DateTime.Now)
                {
                    item.TicketCount = 0;
                    item.Description = "Event ended";
                }
            }

            var eventsPage = events
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItemCount"] = events.Count();

            return View(eventsPage.ToListViewModel());
        }
        [HttpPost]
        public IActionResult Index(SearchEventsModel model)
        {
            int? page = 1;
            var pageNumber = page ?? 1;
            IQueryable<Event> events = null;
            var pageSize = 6;
            if (model.LocationId == 9999)
            {
                events = _context.Events
              .Include(e => e.CreatedBy)
              .Include(e => e.Images)
              .Include(e => e.EventType)
              .Include(e => e.Location)
              .Include(e => e.Tickets)
              .Where(e => e.When >= model.From && e.When <= model.To && e.EventTypeId == model.EventTypeId)
              .OrderByDescending(e => e.Counter)
              .ThenByDescending(e => e.When);
            }
            else
            {
                events = _context.Events
                   .Include(e => e.CreatedBy)
                   .Include(e => e.Images)
                   .Include(e => e.EventType)
                   .Include(e => e.Location)
                   .Include(e => e.Tickets)
                   .Where(e => e.LocationId == model.LocationId && e.When >= model.From && e.When <= model.To && e.EventTypeId == model.EventTypeId)
                   .OrderByDescending(e => e.Counter)
                   .ThenByDescending(e => e.When);
            }
            foreach (var item in events)
            {
                if (item.When <= DateTime.Now)
                {
                    item.TicketCount = 0;
                    item.Description = "Event ended";
                }
            }
            var eventsPage = events
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItemCount"] = events.Count();

            if (eventsPage.Count != 0)
            {
                return View(eventsPage.ToListViewModel());
            }
            else
            {
                var eventt = new EventListItemViewModel() { ErrorMessage = $"No events from {model.From} to {model.To} for your criretia" };
                var list = new List<EventListItemViewModel>
                {
                    eventt
                };
                return View(list);
            }

        }

        private IEnumerable<SelectListItem> GetLocationsList()
        {
            var locations = new List<SelectListItem>();
            foreach (var s in _context.Locations.ToList())
            {
                var location = new SelectListItem { Text = s.Name, Value = s.Id.ToString() };
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
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            if (!HasPermission("CREATE_EVENTS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }
            return View(new EventViewModel { Locations = GetLocationsList(), EventTypes = GetEventTypesList(), When = DateTime.Now });
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventViewModel model)
        {
            if (!HasPermission("CREATE_EVENTS"))
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
                    model.Images = new List<EventImage>();
                    foreach (var formFile in model.ImagesFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            var file = UploadedFile(formFile);
                            var image = new EventImageViewModel()
                            {
                                CreatedById = userId,
                                ImageData = file,
                                CreatedOn = DateTime.Now

                            };
                            model.Images.Add(image.ToEntity());
                        }
                    }
                    var eventt = model.ToEntity();

                    _context.Events.Add(eventt);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
            }

            model.Locations = GetLocationsList();
            model.EventTypes = GetEventTypesList();
            model.When = DateTime.Now;

            return View(model);
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
            var eventt = await _context.Events
                .Include(e => e.CreatedBy)
                .Include(e => e.Images)
                .Include(e => e.EventType)
                .Include(e => e.Location)
                .Include(e => e.Messages)
                .ThenInclude(e => e.CreatedBy)
                .SingleAsync(e => e.ID == id);
            bool isAuthenticated = User.Identity.IsAuthenticated;

            if (eventt == null)
            {
                _logger.LogError("Error showing event details");
                return Error("Error showing event details", "Events"); ;
            }
            if (isAuthenticated)
            {
                userId = GetCurrentUserId().ToString();
                existLog = await _context.Logs.AnyAsync(l => l.UserId == userId && l.EventId == id && l.Type == LogType.EventCounter);
                if (!existLog)
                {
                    var log = new Log()
                    {
                        EventId = id,
                        UserId = userId,
                        Type = LogType.EventCounter,
                        CreatedOn = DateTime.Now
                    };
                    _context.Logs.Add(log);
                    eventt.Counter++;
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                userId = Request.Cookies["VisitorId"];
                existLog = await _context.Logs.AnyAsync(l => l.UserId == userId && l.EventId == id && l.Type == LogType.EventCounter);
                if (!existLog)
                {
                    var log = new Log()
                    {
                        EventId = id,
                        UserId = userId,
                        Type = LogType.EventCounter,
                        CreatedOn = DateTime.Now
                    };
                    _context.Logs.Add(log);
                    eventt.Counter++;
                    await _context.SaveChangesAsync();
                }
            }

            return View(eventt.ToDetailsViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> HideShow(int id)
        {
            var eventt = await _context.Events
                .Include(e => e.CreatedBy)
                .Include(e => e.Images)
                .Include(e => e.EventType)
                .Include(e => e.Location)
                .Include(e => e.Messages)
                .ThenInclude(e => e.CreatedBy)
                .SingleAsync(e => e.ID == id);
            bool isAuthenticated = User.Identity.IsAuthenticated;


            if (eventt == null)
            {
                _logger.LogError("Error changing chat visability");
                return Error("Error changing chat visability","Events" );
            }
            if (eventt.VisibleChat == false)
            {
                eventt.VisibleChat = true;
            }
            else
            {
                eventt.VisibleChat = false;
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Events", new { id });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message, string page)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message, Page = page }); ;
        }
    }
}
