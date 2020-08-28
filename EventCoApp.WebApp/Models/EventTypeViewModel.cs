using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class EventTypeViewModel
    {
        [Display(Name= "Event Type")]
        public int Id { get; set; }

        [Display(Name = "Event Type")]
        public string Name { get; set; }
    }

    public class EventTypeDetailsViewModel
    {
        [Display(Name = "Event Type")]
        public int Id { get; set; }

        [Display(Name = "Event Type")]
        public string Name { get; set; }
    }

    public class EventTypeListItemViewModel
    {
        [Display(Name = "Event Type")]
        public int Id { get; set; }

        [Display(Name = "Event Type")]
        public string Name { get; set; }
    }
}
