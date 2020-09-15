using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class ProfilePictureModel
    {
        [Required]
        public IFormFile PhotoFile { get; set; }
        public string PhotoString { get; set; }
    }
}
