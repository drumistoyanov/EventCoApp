using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static NewsImageViewModel ToViewModel(this NewsImage source)
        {
           NewsImageViewModel destination = new NewsImageViewModel
           {
                ImageData = source.ImageData,
                NewsId = source.NewsId,

                CreatedOn = source.CreatedOn
            };

            return destination;
        }

        public static NewsImageDetailsViewModel ToDetailsViewModel(this NewsImage source)
        {
            NewsImageDetailsViewModel destination = new NewsImageDetailsViewModel
            {
                ImageData = source.ImageData,
                NewsId = source.NewsId
            };


            return destination;
        }

        public static NewsImage ToEntity(this NewsImageViewModel source)
        {
            NewsImage destination = new NewsImage
            {
                ImageData = source.ImageData,
                NewsId = source.NewsId,
                CreatedById = source.CreatedById,
                CreatedOn = source.CreatedOn
            };

            return destination;
        }

        public static NewsImage UpdateEntity(this NewsImageViewModel source, NewsImage destination)
        {
            if (destination == null)
            {
                destination = new NewsImage();
            }

            destination.ImageData = source.ImageData;
            destination.NewsId = source.NewsId;
            destination.CreatedById = source.CreatedById;

            return destination;
        }

        public static List<NewsImageListItemViewModel> ToListViewModel(this List<NewsImage> source)
        {
            List<NewsImageListItemViewModel> imageListItems = new List<NewsImageListItemViewModel>();

            if (source != null)
            {
                foreach (NewsImage item in source)
                {
                    imageListItems.Add(item.ToListItemViewModel());
                }
            }

            return imageListItems;
        }

        public static NewsImageListItemViewModel ToListItemViewModel(this NewsImage source)
        {
            NewsImageListItemViewModel image = new NewsImageListItemViewModel
            {
                ImageData = source.ImageData,
                NewsId = source.NewsId,
                CreatedById = source.CreatedById.Value
            };

            return image;
        }
    }
}
