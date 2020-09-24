using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EventCoApp.WebApp.Models
{
    public class EventViewModel
    {
        [Display(Name = "Event Id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Event Name")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }
        [Display(Name = "Locations")]
        public IEnumerable<SelectListItem> Locations { get; set; }
        [Required]
        [Display(Name = "When Is")]
        public DateTime When { get; set; }
        [Display(Name = "Images")]
        public List<EventImage> Images { get; set; }
        public List<IFormFile> ImagesFiles { get; set; }
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
        [Display(Name = "Ticket Count")]
        public int TicketCount { get; set; }
        [Display(Name = "Created By")]
        public string UserName { get; set; }
        public int CreatedById { get; set; }
        [Display(Name = "Seen")]
        public int Counter { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int EventTypeId { get; set; }
        [Display(Name = "Types")]
        public IEnumerable<SelectListItem> EventTypes { get; set; }
    }

    public class EventDetailsViewModel
    {
        [Display(Name = "Event Id")]
        public int Id { get; set; }
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "When Is")]
        public DateTime When { get; set; }
        public List<EventImageListItemViewModel> Images { get; set; }
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
        [Display(Name = "Ticket Count")]
        public int TicketCount { get; set; }
        [Display(Name = "Seen")]
        public int Counter { get; set; }

        [Display(Name = "Created By")]
        public string UserName { get; set; }

        [Display(Name = "Type")]
        public EventTypeListItemViewModel EventType { get; set; }
        public List<MessageListViewModel> Messages { get; set; }
        public bool VisibleChat { get; set; }
    }

    public class EventListItemViewModel
    {
        [Display(Name = "Event Id")]
        public int Id { get; set; }
        [Display(Name = "Event Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }

        [Display(Name = "When Is")]
        public DateTime When { get; set; }
        public List<EventImageListItemViewModel> Images { get; set; }
        public List<IFormFile> ImagesFiles { get; set; }
        [Display(Name = "Ticket Price")]
        public decimal TicketPrice { get; set; }
        [Display(Name = "Seen")]
        public int Counter { get; set; }
        [Display(Name = "Ticket Count")]
        public int TicketCount { get; set; }

        [Display(Name = "Created By")]
        public string UserName { get; set; }

        [Display(Name = "Type")]
        public EventTypeListItemViewModel EventType { get; set; }

        public string ErrorMessage { get; set; }
    }
}
