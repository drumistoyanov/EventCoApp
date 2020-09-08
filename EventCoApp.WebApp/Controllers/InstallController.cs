using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EventCoApp.Common.Enums;
using EventCoApp.Common.Helpers;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;

namespace EventCoApp.WebApp.Controllers
{
    public class InstallController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private readonly EventCoContext _context;

        public InstallController(
            EventCoContext context,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ILoggerFactory loggerFactory,
            IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<InstallController>();
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(InstallationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventTypes = new List<EventType>() {
                    new EventType() { Name = "CultureEvent" },
                    new EventType() { Name = "DonationEvent" },
                    new EventType() { Name = "SportEvent" },
                    new EventType() { Name = "VolunteersEvent" }
                };

                _context.EventTypes.AddRange(eventTypes);


                var permissions = new List<Permission>()
                {
                    new Permission() { Group = "events", Name = "CREATE_EVENTS", Description = "Create Events" },
                    new Permission() { Group = "events", Name = "EDIT_EVENTS", Description = "Edit Events" },
                    new Permission() { Group = "events", Name = "VIEW_EVENTS", Description = "View Events" },
                    new Permission() { Group = "events", Name = "DELETE_EVENTS", Description = "Delete Events" },
                    new Permission() { Group = "bookings", Name = "CREATE_BOOKINGS", Description = "Create Bookings" },
                    new Permission() { Group = "bookings", Name = "VIEW_BOOKINGS", Description = "View Bookings" },
                    new Permission() { Group = "settings", Name = "CREATE_ROLES", Description = "Create Roles" },
                    new Permission() { Group = "settings", Name = "EDIT_ROLES", Description = "Edit Roles" },
                    new Permission() { Group = "settings", Name = "VIEW_ROLES", Description = "View Roles" },
                    new Permission() { Group = "settings", Name = "DELETE_ROLES", Description = "Delete Roles" },
                     new Permission() { Group = "news", Name = "CREATE_NEWS", Description = "Create News" },
                    new Permission() { Group = "news", Name = "EDIT_NEWS", Description = "Edit News" },
                    new Permission() { Group = "news", Name = "VIEW_NEWS", Description = "View News" },
                    new Permission() { Group = "news", Name = "DELETE_NEWS", Description = "Delete News" },
                };

                _context.Permissions.AddRange(permissions);


                //var roles = new List<Role>()
                //{
                //    new Role()  { Name = "Administrator"},
                //    new Role()  { Name="Event_Creator"},
                //    new Role()  { Name="Normal_User"}
                //};
                _context.Locations.AddRange(LoadJson());
                var user = new User
                {
                    UserName = model.UserName,
                    UserType = UserType.MASTER,
                    FirstName = model.UserName,
                    LastName = model.UserName,
                    Email = model.UserName,
                    DateOfRegistration = DateTime.Now,
                    EmailConfirmed = true
                };


                _context.SaveChanges();
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var adminRole = new Role()
                    {
                        Name = UserType.MASTER.GetAttribute<DisplayAttribute>().Name
                    };

                    result = await _roleManager.CreateAsync(adminRole);

                    if (result.Succeeded)
                    {
                        foreach (var _permission in _context.Permissions)
                        {
                            _context.RolePermissions.Add(new RolePermission()
                            {
                                RoleId = adminRole.Id,
                                PermissionId = _permission.Id
                            });
                        }

                        _context.SaveChanges();

                        await _userManager.AddToRoleAsync(user, adminRole.Name);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            return View(model);
        }

        public List<Location> LoadJson()
        {
            var listLocations = new List<Location>();
            string webRootPath = _env.WebRootPath;
            var path = webRootPath + @"\install\bg.json";
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var list = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (var item in list)
                {
                    var location = new Location
                    {
                        Name = item.city
                    };
                    listLocations.Add(location);
                }
            }
            return listLocations;
        }
        public class Item
        {
            public string city;
            public string admin;
            public string country;
            public int? population_proper;
            public string iso2;
            public string capital;
            public decimal? lat;
            public decimal? lng;
            public int? population;
        }
    }
}
