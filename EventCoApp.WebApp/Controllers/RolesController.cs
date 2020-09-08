using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using EventCoApp.Common.Helpers;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SwiftCourier.Models.Extensions;

namespace EventCoApp.WebApp.Controllers
{
    public class RolesController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(
            UserManager<User> userManager, RoleManager<Role> roleManager, EventCoContext context, ILogger<RolesController> logger) : base(userManager, context)
        {
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (!HasPermission("VIEW_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var roles = _roleManager.Roles;
            return View(await roles.Include(r => r.RolePermissions).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!HasPermission("VIEW_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (id == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            var role = await _roleManager.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == id);

            if (role == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            return View(role);
        }

        public IActionResult Create()
        {
            if (!HasPermission("CREATE_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            ViewBag.PermissionGroups = PermissionGroup.GetList();
            ViewBag.Permissions = _context.Permissions.ToList().ToListViewModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (!HasPermission("CREATE_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (ModelState.IsValid)
            {
                var role = model.ToEntity();

                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogWarning(string.Join(',',result.Errors));
                    AddErrors(result);
                }
            }

            ViewBag.PermissionGroups = PermissionGroup.GetList();
            ViewBag.Permissions = _context.Permissions.ToList().ToListViewModel();

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!HasPermission("EDIT_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (id == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            var role = await _roleManager.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == id.Value);

            if (role == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            ViewBag.PermissionGroups = PermissionGroup.GetList();
            ViewBag.Permissions = _context.Permissions.ToList().ToListViewModel();

            return View(role.ToViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RoleViewModel model)
        {
            if (!HasPermission("EDIT_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var role = await _roleManager.Roles.Include(r => r.RolePermissions).FirstOrDefaultAsync(r => r.Id == model.Id);

            if (role == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            if (ModelState.IsValid)
            {
                role = model.UpdateEntity(role);

                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogWarning("Not found");
                    AddErrors(result);
                }
            }

            ViewBag.PermissionGroups = PermissionGroup.GetList();
            ViewBag.Permissions = _context.Permissions.ToList().ToListViewModel();

            return View(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!HasPermission("DELETE_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            if (id == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                _logger.LogWarning("Not found");
                return Error("Not found", "Roles");
            }

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!HasPermission("DELETE_ROLES"))
            {
                _logger.LogInformation("AccessDenied");
                return AccessDenied();
            }

            var role = await _roleManager.FindByIdAsync(id.ToString());

            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message, string page)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message, Page = page }); ;
        }
    }
}
