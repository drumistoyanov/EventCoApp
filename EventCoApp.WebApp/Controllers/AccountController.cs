using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.Services.Messaging;
using EventCoApp.WebApp.Areas.Identity.Pages.Account.Manage;
using EventCoApp.WebApp.Models;
using EventCoApp.WebApp.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        public AccountController(
            UserManager<User> userManager, IWebHostEnvironment hostEnvironment, EventCoContext context) : base(userManager, context, hostEnvironment)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ProfilePictureModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }
                if (model.PhotoFile.Length > 0)
                {
                    var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                    var file = UploadedFile(model.PhotoFile);
                    userEntity.ProfilePicture = file;
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index","Home");
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

    }
}
