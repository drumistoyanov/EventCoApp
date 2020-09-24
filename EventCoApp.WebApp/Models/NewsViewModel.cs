using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Images")]
        public List<NewsImage> Images { get; set; }
        public List<IFormFile> ImagesFiles { get; set; }
        public bool Approved { get; set; } = false;
        public int Counter { get; set; } = 0;
        public string Name { get; set; }

        public string CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public DateTime CreatedOn { get; set; }

    }
    public class NewsDetailsViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Images")]
        public List<NewsImageListItemViewModel> Images { get; set; }
        public bool Approved { get; set; } = false;
        public int Counter { get; set; } = 0;
        public string Name { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

    }

    public class NewsListItemViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Images")]
        public List<NewsImageListItemViewModel> Images { get; set; }
        public bool Approved { get; set; } = false;
        public int Counter { get; set; } = 0;
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }
    }
}
