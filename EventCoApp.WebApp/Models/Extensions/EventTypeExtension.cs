using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCoApp.DataAccessLibrary.Models;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static EventType ToEntity(this EventTypeViewModel source)
        {
            var destination = new EventType();

            destination.Name = source.Name;

            return destination;
        }

        public static EventType UpdateEntity(this EventTypeViewModel source, EventType destination)
        {
            if(destination == null)
            {
                destination = new EventType();
            }

            if(source.Id != 0)
            {
                destination.Id = source.Id;
            }

            destination.Name = source.Name;

            return destination;
        }

        public static EventTypeViewModel ToViewModel(this EventType source)
        {
            var destination = new EventTypeViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static EventTypeDetailsViewModel ToDetailsViewModel(this EventType source)
        {
            var destination = new EventTypeDetailsViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static EventTypeListItemViewModel ToListItemViewModel(this EventType source)
        {
            var destination = new EventTypeListItemViewModel();

            destination.Id = source.Id;
            destination.Name = source.Name;

            return destination;
        }

        public static List<EventTypeListItemViewModel> ToListViewModel(this List<EventType> source)
        {
            var destination = new List<EventTypeListItemViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }
    }
}
