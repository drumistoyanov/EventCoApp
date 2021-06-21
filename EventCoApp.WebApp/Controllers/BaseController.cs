using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, EventCoContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public BaseController(UserManager<User> userManager, EventCoContext context, IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        protected readonly IHttpContextAccessor _accessor;
        protected readonly IDistributedCache _cache;
        protected readonly UserManager<User> _userManager;
        protected EventCoContext _context;
        protected readonly IWebHostEnvironment _hostingEnvironment;

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        protected async Task<User> GetCurrentUserAsync()
        {
            //return await _userManager.GetUserAsync(HttpContext.User);
            return await _userManager.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id.ToString() == _userManager.GetUserId(HttpContext.User));
        }

        protected int GetCurrentUserId()
        {
            var task = GetCurrentUserAsync();

            var user = task.Result;

            if (user == null)
            {
                throw new Exception("Unable to get id of current user.");
            }

            return user.Id;
        }

        protected bool HasPermission(string permissionName)
        {
            var task = GetCurrentUserAsync();

            var user = task.Result;

            if (user == null)
            {
                return false;
            }

            var permission = _context.Permissions.Include(p => p.RolePermissions).FirstOrDefault(p => p.Name == permissionName);

            if (permission == null)
            {
                return false;
            }

            var roleIds = new List<int>();

            foreach (var rolePermission in permission.RolePermissions)
            {
                roleIds.Add(rolePermission.RoleId);
            }

            var userRoles = _context.UserRoles.Where(u => u.UserId == user.Id);

            foreach (var userRole in userRoles)
            {
                if (roleIds.Contains(userRole.RoleId))
                {
                    return true;
                }
            }

            return false;
        }

        public override UnauthorizedResult Unauthorized()
        {
            var result = new UnauthorizedResult();

            return result;
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.IndexAsync), "Home");
            }
        }
    }
}
