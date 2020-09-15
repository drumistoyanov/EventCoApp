using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static BookingViewModel ToViewModel(this Booking source)
        {
            var destination = new BookingViewModel
            {
                Id = source.ID,

                RequestDate = source.CreatedOn,
                EventId = source.EventId,
                TicketCount = source.TicketCount,
                WholePrice = source.WholePrice  
            };


            return destination;
        }

        public static BookingListItemViewModel ToListItemViewModel(this Booking source)
        {
            var destination = new BookingListItemViewModel
            {
                Id = source.ID,
                Event=source.Event.ToDetailsViewModel(),
                RequestDate = source.CreatedOn,
                EventId = source.EventId,
                TicketCount = source.TicketCount,
                WholePrice = source.WholePrice,
                TicketPrice=source.Event.TicketPrice
            };


            return destination;
        }

        public static List<BookingListItemViewModel> ToListViewModel(this List<Booking> source)
        {
            var destination = new List<BookingListItemViewModel>();

            if (source != null)
            {
                foreach (var item in source)
                {
                    destination.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }

        public static BookingDetailsViewModel ToDetailsViewModel(this Booking source)
        {
            var destination = new BookingDetailsViewModel
            {
                When = source.Event.When,
                Event = source.Event.ToDetailsViewModel(),
                TicketPrice=source.Event.TicketPrice,
                TicketCount = source.TicketCount,
                WholePrice = source.WholePrice
            };


            return destination;
        }

        public static Booking ToEntity(this BookingViewModel source)
        {
            var destination = new Booking()
            {
                CreatedOn = source.RequestDate,
                EventId = source.EventId,
                TicketCount = source.TicketCount,
                WholePrice = source.TicketCount * source.TicketPrice
            };

            return destination;
        }

        public static Booking UpdateEntity(this BookingViewModel source, Booking destination)
        {
            destination.CreatedOn = source.RequestDate;
            destination.EventId = source.EventId;
            destination.TicketCount = source.TicketCount;
            destination.WholePrice = source.TicketCount * source.TicketPrice;

            return destination;
        }
    }
}
