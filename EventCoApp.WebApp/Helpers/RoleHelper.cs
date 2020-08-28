using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventCoApp.Common
{
    public static class RoleHelper
    {
        private static async Task EnsureRoleCreated(RoleManager<Role> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role(roleName));
            }
        }
        public static async Task EnsureRolesCreated(this RoleManager<Role> roleManager)
        {
            // add all roles, that should be in database, here
            await EnsureRoleCreated(roleManager, "Admin");
            await EnsureRoleCreated(roleManager, "Guest");
            await EnsureRoleCreated(roleManager, "User");
        }

    }
}
