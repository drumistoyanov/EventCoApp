using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.Common.ViewModels.Admin.Users
{
    public class UserDetailsViewModel
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfRegistration { get; set; }
    }
}
