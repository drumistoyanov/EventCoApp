using AutoMapper;
using EventCoApp.Common.BindingModels.Admin.Users;
using EventCoApp.Common.ViewModels.Admin.Users;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.Services.Admin.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventCoApp.Services.Admin
{
    public class AdminUsersService : BaseEFService, IAdminUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AdminUsersService(EventCoContext dbContext,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
            : base(dbContext, mapper)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<IEnumerable<UserIndexViewModel>> GetUsers(ClaimsPrincipal sessionUser)
        {
            var currentUser = await _userManager.GetUserAsync(sessionUser);
            var users = DbContext.Users
                .Where(u => u.Id != currentUser.Id)
                .ToList();

            return Mapper.Map<IEnumerable<UserIndexViewModel>>(users);
        }

        public async Task<UserDetailsViewModel> GetUserDatails(string id)
        {
            var user = await DbContext.Users.FindAsync(id);
            CheckIfUserExist(user);

            var model = Mapper.Map<UserDetailsViewModel>(user);

            return model;
        }

        public async Task BanUser(string id)
        {
            var user = await DbContext.Users.FindAsync(id);
            CheckIfUserExist(user);
            user.LockoutEnd = DateTime.UtcNow.AddYears(100);

            DbContext.SaveChanges();
        }

        public async Task<IdentityResult> ChangeUserPassword(string id, ChangePasswordBindingModel model)
        {
            var user = await DbContext.Users.FindAsync(id);
            CheckIfUserExist(user);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);

            return await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(string id)
        {
            var user = await DbContext.Users.FindAsync(id);
            CheckIfUserExist(user);

            await _userManager.DeleteAsync(user);

            DbContext.SaveChanges();
        }

        private void CheckIfUserExist(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"User with {user.Id} id not found!");
            }
        }
    }
}
