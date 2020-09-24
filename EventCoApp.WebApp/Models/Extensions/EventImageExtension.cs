using EventCoApp.DataAccessLibrary.Models;
using System.Collections.Generic;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static EventImageViewModel ToViewModel(this EventImage source)
        {
            EventImageViewModel destination = new EventImageViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId,

                CreatedOn = source.CreatedOn
            };

            return destination;
        }

        public static EventImageDetailsViewModel ToDetailsViewModel(this EventImage source)
        {
            EventImageDetailsViewModel destination = new EventImageDetailsViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId
            };


            return destination;
        }

        public static EventImage ToEntity(this EventImageViewModel source)
        {
            EventImage destination = new EventImage
            {
                ImageData = source.ImageData,
                EventId = source.EventId,
                CreatedById = source.CreatedById,
                CreatedOn = source.CreatedOn
            };

            return destination;
        }

        public static EventImage UpdateEntity(this EventImageViewModel source, EventImage destination)
        {
            if (destination == null)
            {
                destination = new EventImage();
            }

            destination.ImageData = source.ImageData;
            destination.EventId = source.EventId;
            destination.CreatedById = source.CreatedById;

            return destination;
        }

        public static List<EventImageListItemViewModel> ToListViewModel(this List<EventImage> source)
        {
            List<EventImageListItemViewModel> imageListItems = new List<EventImageListItemViewModel>();

            if (source != null)
            {
                foreach (EventImage item in source)
                {
                    imageListItems.Add(item.ToListItemViewModel());
                }
            }

            return imageListItems;
        }

        public static EventImageListItemViewModel ToListItemViewModel(this EventImage source)
        {
            EventImageListItemViewModel image = new EventImageListItemViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId,
                CreatedById = source.CreatedById.Value
            };

            return image;
        }
    }
}
