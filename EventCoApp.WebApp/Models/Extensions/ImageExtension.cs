using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static ImageViewModel ToViewModel(this Image source)
        {
            var destination = new ImageViewModel();

            destination.ImageData = source.ImageData;
            destination.EventId = source.EventId;

            destination.CreatedOn = source.CreatedOn;

            return destination;
        }

        public static ImageDetailsViewModel ToDetailsViewModel(this Image source)
        {
            var destination = new ImageDetailsViewModel();


            destination.ImageData = source.ImageData;
            destination.EventId = source.EventId;


            return destination;
        }

        public static Image ToEntity(this ImageViewModel source)
        {
            var destination = new Image();


            destination.ImageData = source.ImageData;
            destination.EventId = source.EventId;
            destination.CreatedById = source.CreatedById;
            destination.CreatedOn = source.CreatedOn;

            return destination;
        }

        public static Image UpdateEntity(this ImageViewModel source, Image destination)
        {
            if (destination == null)
            {
                destination = new Image();
            }

            destination.ImageData = source.ImageData;
            destination.EventId = source.EventId;
            destination.CreatedById = source.CreatedById;

            return destination;
        }

        public static List<ImageListItemViewModel> ToListViewModel(this List<Image> source)
        {
            var imageListItems = new List<ImageListItemViewModel>();

            if (source != null)
            {
                foreach (var item in source)
                {
                    imageListItems.Add(item.ToListItemViewModel());
                }
            }

            return imageListItems;
        }

        public static ImageListItemViewModel ToListItemViewModel(this Image source)
        {
            var image = new ImageListItemViewModel();

            image.ImageData = source.ImageData;
            image.EventId = source.EventId;
            image.CreatedById = source.CreatedById.Value;

            return image;
        }
    }
}
