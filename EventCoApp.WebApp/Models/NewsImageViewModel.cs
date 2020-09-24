using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class NewsImageViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int NewsId { get; set; }
        public NewsDetailsViewModel News { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
    }

    public class NewsImageDetailsViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int NewsId { get; set; }
        public NewsDetailsViewModel News { get; set; }

        public string UserName { get; set; }
        public int CreatedById { get; set; }
    }

    public class NewsImageListItemViewModel
    {
        [Required]
        public string ImageData { get; set; }
        public int NewsId { get; set; }
        public NewsDetailsViewModel News { get; set; }
        public string UserName { get; set; }
        public int CreatedById { get; set; }
    }
}
