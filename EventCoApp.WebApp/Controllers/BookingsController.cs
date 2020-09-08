using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventCoApp.WebApp.Controllers
{
    public class BookingsController : BaseController
    {
        private readonly ILogger<EventsController> _logger;
        public BookingsController(UserManager<User> userManager, EventCoContext context, ILogger<EventsController> logger) : base(userManager, context)
        {
            _logger = logger;
        }

        [Authorize]
        public IActionResult Index(int? page)
        {
            if (!HasPermission("VIEW_BOOKINGS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var pageNumber = page ?? 1;

            var pageSize = 50;
            int userId = GetCurrentUserId();
            var bookings = _context.Bookings
                .Include(b => b.Event)
                .ThenInclude(b => b.Location)
                    .Include(b => b.Event)
                .ThenInclude(b => b.CreatedBy)
                .OrderByDescending(b => b.CreatedOn)
                .Where(b => b.CreatedById == userId);

            var bookingsPage = bookings
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalItemCount"] = bookings.Count();

            return View(bookingsPage.ToListViewModel());
        }

        [Authorize(Roles = "Creator, User, Master")]
        public async Task<IActionResult> Details(int id)
        {
            if (!HasPermission("VIEW_BOOKINGS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var booking = await _context.Bookings
                .Include(e => e.Event)
                .ThenInclude(e => e.CreatedBy)
                .Include(e => e.Event)
                .ThenInclude(e => e.Location)
                .Include(e => e.CreatedBy)
                .SingleAsync(b => b.ID == id);
            if (booking == null)
            {
                _logger.LogError("Error showing booking details");
                return Error("Error showing booking details", "Bookings"); ;

            }

            return View(booking.ToDetailsViewModel());
        }


        [Authorize]
        public IActionResult Create(int id)
        {
            if (!HasPermission("CREATE_BOOKINGS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var @event = _context.Events
                .Include(e => e.Location)
                .Include(e => e.CreatedBy)
                .FirstOrDefault(e => e.ID == id).ToDetailsViewModel();
            var ticketPrice = _context.Events
                .FirstOrDefault(e => e.ID == id).TicketPrice;

            var ticketsLeft = _context.Events
                .FirstOrDefault(e => e.ID == id).TicketCount;


            return View(new BookingViewModel() { Event = @event, TicketCountLeft = ticketsLeft, TicketPrice = ticketPrice });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (!HasPermission("CREATE_BOOKINGS"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (ModelState.IsValid)
            {
                var @event = await _context.Events.Include(x => x.Location).Include(x => x.CreatedBy).FirstAsync(x => x.ID == model.Id);
                var ticketPrice = _context.Events
               .FirstOrDefault(e => e.ID == model.Id).TicketPrice;

                var ticketsLeft = _context.Events
                    .FirstOrDefault(e => e.ID == model.Id).TicketCount;
                model.TicketPrice = ticketPrice;
                model.TicketCountLeft = ticketsLeft;
                model.Event = @event.ToDetailsViewModel();
                if (ticketsLeft < model.TicketCount)
                {
                    _logger.LogError("Wrong tickets count");
                    ModelState.AddModelError("Wrong", "Please Select");

                    return View(new BookingViewModel() { Event = @event.ToDetailsViewModel(), TicketCountLeft = ticketsLeft, TicketPrice = ticketPrice });
                }
                model.EventId = @event.ID;
                var booking = model.ToEntity();

                booking.CreatedOn = DateTime.Now;

                int userId = GetCurrentUserId();

                booking.CreatedById = userId;

                _context.Bookings.Add(booking);
                @event.TicketCount = ticketsLeft - model.TicketCount;

                await _context.SaveChangesAsync();


                return RedirectToAction("Details", "Bookings", new { id = booking.ID });
            }

            var @event1 = _context.Events
              .Include(e => e.Location)
              .Include(e => e.CreatedBy)
              .FirstOrDefault(e => e.ID == model.Id).ToDetailsViewModel();
            var ticketPrice1 = _context.Events
                .FirstOrDefault(e => e.ID == model.Id).TicketPrice;

            var ticketsLeft1 = _context.Events
                .FirstOrDefault(e => e.ID == model.Id).TicketCount;


            return View(new BookingViewModel() { Event = @event1, TicketCountLeft = ticketsLeft1, TicketPrice = ticketPrice1 });
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (!HasPermission("EDIT_BOOKINGS"))
        //    {
        //        return Unauthorized();
        //    }

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    Booking booking = await _context.Bookings
        //        .Include(b => b.Invoice)
        //        .Include(b => b.Package)
        //        .Include(b => b.Package.PackageType)
        //        .SingleAsync(m => m.Id == id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = booking.ToViewModel();

        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
        //    ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);
        //    ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(BookingViewModel model)
        //{
        //    if (!HasPermission("EDIT_BOOKINGS"))
        //    {
        //        return Unauthorized();
        //    }

        //    if (ModelState.IsValid)
        //    {

        //        var booking = await _context.Bookings
        //            .Include(b => b.Invoice)
        //            .Include(b => b.Package)
        //            .Include(b => b.Package.PackageType)
        //            .SingleAsync(m => m.Id == model.Id);

        //        booking = model.UpdateEntity(booking);

        //        if (booking.Invoice.Status == InvoiceStatus.NotIssued)
        //        {
        //            booking.Invoice.Status = InvoiceStatus.Pending;
        //        }

        //        if (booking.Invoice.AmountPaid >= booking.Invoice.Total)
        //        {
        //            booking.Invoice.Status = InvoiceStatus.Paid;
        //            booking.Invoice.AmountDue = 0;
        //        }

        //        _context.Update(booking);

        //        await _context.SaveChangesAsync();

        //        return RedirectToAction("Index");
        //    }

        //    ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Name");
        //    ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", model.ServiceId);
        //    ViewData["PackageTypeId"] = new SelectList(_context.PackageTypes, "Id", "Name");

        //    return View(model);
        //}

        //[ActionName("Delete")]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (!HasPermission("DELETE_BOOKINGS"))
        //    {
        //        return Unauthorized();
        //    }

        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var booking = await _context.Bookings
        //        .Include(b => b.Origin)
        //        .Include(b => b.Destination)
        //        .Include(b => b.Customer)
        //        .Include(b => b.Invoice)
        //        .Include(b => b.Package)
        //        .Include(b => b.Package.PackageType)
        //        .Include(b => b.Service)
        //        .SingleAsync(m => m.Id == id);

        //    if (booking == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(booking.ToDetailsViewModel());
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (!HasPermission("DELETE_BOOKINGS"))
        //    {
        //        return Unauthorized();
        //    }

        //    Booking booking = await _context.Bookings.SingleAsync(m => m.Id == id);
        //    _context.Bookings.Remove(booking);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message, string page)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message, Page = page }); ;
        }
    }
}

