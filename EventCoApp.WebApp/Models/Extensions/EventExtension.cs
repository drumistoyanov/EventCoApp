using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCoApp.DataAccessLibrary.Models;
using System.Security.Cryptography;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static EventViewModel ToViewModel(this Event source)
        {
            var destination = new EventViewModel
            {
                Id = source.ID,
                Name = source.Name,
                Description = source.Description,
                LocationId = source.LocationId,
                When = source.When,
                CreatedById = source.CreatedById.Value
            };


            if (source.EventType != null)
            {
                destination.EventTypeId = source.EventTypeId;
            }

            return destination;
        }

        public static EventDetailsViewModel ToDetailsViewModel(this Event source)
        {
            var destination = new EventDetailsViewModel
            {
                Id = source.ID,
                Name = source.Name,
                Description = source.Description,
                Location = source.Location.Name,
                When = source.When,
                UserName = source.CreatedBy.UserName,
                Counter = source.Counter,
                VisibleChat = source.VisibleChat
            };

            if (source.Messages != null)
            {
                destination.Messages = new List<MessageListViewModel>();
                foreach (var item in source.Messages)
                {
                    destination.Messages.Add(item.ToListItemViewModel());
                }
            }

            if (source.EventType != null)
            {
                destination.EventType = source.EventType.ToListItemViewModel();
            }
            if (source.Images != null)
            {
                destination.Images = new List<EventImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    destination.Images.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }

        public static Event ToEntity(this EventViewModel source)
        {
            var destination = new Event
            {
                ID = source.Id,
                Name = source.Name,
                Description = source.Description,
                LocationId = source.LocationId,
                When = source.When,
                CreatedById = source.CreatedById,
                TicketCount = source.TicketCount,
                TicketPrice = source.TicketPrice,

                CreatedOn = DateTime.Now
            };
            if (source.Images!=null)
            {

                foreach (var image in source.Images)
                {
                    destination.Images.Add(image);
                }
            }

            destination.EventTypeId = source.EventTypeId;

            return destination;
        }

        public static Event UpdateEntity(this EventViewModel source, Event destination)
        {
            if (destination == null)
            {
                destination = new Event();
            }

            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.LocationId = source.LocationId;
            destination.When = source.When;

            destination.EventTypeId = source.EventTypeId;

            return destination;
        }

        public static List<EventListItemViewModel> ToListViewModel(this List<Event> source)
        {
            var events = new List<EventListItemViewModel>();

            if (source != null)
            {
                foreach (var item in source)
                {
                    events.Add(item.ToListItemViewModel());
                }
            }

            return events;
        }

        public static EventListItemViewModel ToListItemViewModel(this Event source)
        {
            var destination = new EventListItemViewModel
            {
                Id = source.ID,
                Name = source.Name,
                UserName = source.CreatedBy != null ? source.CreatedBy.UserName : "Customer",
                Description = source.Description,
                Location = source.Location.Name,
                When = source.When,
                TicketCount = source.TicketCount,
                TicketPrice = source.TicketPrice,
                Counter = source.Counter
            };


            if (source.EventType != null)
            {
                destination.EventType = source.EventType.ToListItemViewModel();
            }
            if (source.Images!=null)
            {
                destination.Images = new List<EventImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    destination.Images.Add(item.ToListItemViewModel());
                }

            }


            return destination;
        }
    }
}
