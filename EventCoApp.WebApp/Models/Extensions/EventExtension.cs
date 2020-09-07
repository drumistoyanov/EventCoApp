using SwiftCourier.Models;
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
            var destination = new EventViewModel();

            destination.Id = source.ID;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.LocationId = source.LocationId;
            destination.When = source.When;
            destination.CreatedById = source.CreatedById.Value;


            if (source.EventType != null)
            {
                destination.EventTypeId = source.EventTypeId;
            }

            return destination;
        }

        public static EventDetailsViewModel ToDetailsViewModel(this Event source)
        {
            var destination = new EventDetailsViewModel();

            destination.Id = source.ID;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.Location = source.Location.Name;
            destination.When = source.When;
            destination.UserName = source.CreatedBy.UserName;
            destination.Counter = source.Counter;
            destination.VisibleChat = source.VisibleChat;

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
                destination.Images = new List<ImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    destination.Images.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }

        public static Event ToEntity(this EventViewModel source)
        {
            var destination = new Event();

            destination.ID = source.Id;
            destination.Name = source.Name;
            destination.Description = source.Description;
            destination.LocationId = source.LocationId;
            destination.When = source.When;
            destination.CreatedById = source.CreatedById;
            destination.TicketCount = source.TicketCount;
            destination.TicketPrice = source.TicketPrice;

            destination.CreatedOn = DateTime.Now;
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
            var @event = new EventListItemViewModel();

            @event.Id = source.ID;
            @event.Name = source.Name;
            @event.UserName = source.CreatedBy != null ? source.CreatedBy.UserName : "Customer";
            @event.Description = source.Description;
            @event.Location = source.Location.Name;
            @event.When = source.When;
            @event.TicketCount = source.TicketCount;
            @event.TicketPrice = source.TicketPrice;
            @event.Counter = source.Counter;
            

            if (source.EventType != null)
            {
                @event.EventType = source.EventType.ToListItemViewModel();
            }
            if (source.Images!=null)
            {
                @event.Images = new List<ImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    @event.Images.Add(item.ToListItemViewModel());
                }

            }


            return @event;
        }
    }
}
