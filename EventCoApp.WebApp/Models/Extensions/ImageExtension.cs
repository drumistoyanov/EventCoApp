using EventCoApp.DataAccessLibrary.Models;
using System.Collections.Generic;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static ImageViewModel ToViewModel(this Image source)
        {
            ImageViewModel destination = new ImageViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId,

                CreatedOn = source.CreatedOn
            };

            return destination;
        }

        public static ImageDetailsViewModel ToDetailsViewModel(this Image source)
        {
            ImageDetailsViewModel destination = new ImageDetailsViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId
            };


            return destination;
        }

        public static Image ToEntity(this ImageViewModel source)
        {
            Image destination = new Image
            {
                ImageData = source.ImageData,
                EventId = source.EventId,
                CreatedById = source.CreatedById,
                CreatedOn = source.CreatedOn
            };

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
            List<ImageListItemViewModel> imageListItems = new List<ImageListItemViewModel>();

            if (source != null)
            {
                foreach (Image item in source)
                {
                    imageListItems.Add(item.ToListItemViewModel());
                }
            }

            return imageListItems;
        }

        public static ImageListItemViewModel ToListItemViewModel(this Image source)
        {
            ImageListItemViewModel image = new ImageListItemViewModel
            {
                ImageData = source.ImageData,
                EventId = source.EventId,
                CreatedById = source.CreatedById.Value
            };

            return image;
        }
    }
}
