using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class SearchEventsModel
    {
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }
        public int LocationId { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int EventTypeId { get; set; }
        [Display(Name = "Types")]
        public IEnumerable<SelectListItem> EventTypes { get; set; }

        public IEnumerable<NewsViewModel> News { get; set; }
    }
}
