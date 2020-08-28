using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.ViewModels.Admin.Users
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
