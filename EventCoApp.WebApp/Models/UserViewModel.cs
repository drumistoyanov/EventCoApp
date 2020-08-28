﻿using EventCoApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            RoleIds = new List<int>();
        }

        public int Id { get; set; }

        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<int> RoleIds { get; set; }
    }

    public class UserChangePasswordViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
