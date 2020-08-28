using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class ImageViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int EventId { get; set; }
        public EventDetailsViewModel Event { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
    }

    public class ImageDetailsViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int EventId { get; set; }
        public EventDetailsViewModel Event { get; set; }

        public string UserName { get; set; }
        public int CreatedById { get; set; }
    }

    public class ImageListItemViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int EventId { get; set; }
        public EventDetailsViewModel Event { get; set; }
        public string UserName { get; set; }
        public int CreatedById { get; set; }
    }
}
