using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class NewsExtension
    {
        public static NewsViewModel ToViewModel(this News source)
        {
            var destination = new NewsViewModel
            {
                Id = source.ID,
                Name = source.Name,
                Description = source.Description,
                CreatedById = source.CreatedById.Value
            };

            return destination;
        }

        public static NewsDetailsViewModel ToDetailsViewModel(this News source)
        {
            var destination = new NewsDetailsViewModel
            {
                Id = source.ID,
                Name = source.Name,
                Description = source.Description,
                CreatedBy = source.CreatedBy.UserName

            };
            if (source.Images != null)
            {
                destination.Images = new List<NewsImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    destination.Images.Add(item.ToListItemViewModel());
                }
            }

            return destination;
        }

        public static News ToEntity(this NewsViewModel source)
        {
            var destination = new News
            {
                ID = source.Id,
                Name = source.Name,
                Description = source.Description,
                CreatedOn = DateTime.Now,
            };
            if (source.Images != null)
            {

                foreach (var image in source.Images)
                {
                    destination.Images.Add(image);
                }
            }

            return destination;
        }

        public static News UpdateEntity(this NewsViewModel source, News destination)
        {
            if (destination == null)
            {
                destination = new News();
            }

            destination.Name = source.Name;
            destination.Description = source.Description;

            return destination;
        }

        public static List<NewsListItemViewModel> ToListViewModel(this List<News> source)
        {
            var events = new List<NewsListItemViewModel>();

            if (source != null)
            {
                foreach (var item in source)
                {
                    events.Add(item.ToListItemViewModel());
                }
            }

            return events;
        }

        public static NewsListItemViewModel ToListItemViewModel(this News source)
        {
            var destination = new NewsListItemViewModel
            {
                Id = source.ID,
                Name = source.Name,
                Description = source.Description,
                CreatedOn = source.CreatedOn
            };

            if (source.Images != null)
            {
                destination.Images = new List<NewsImageListItemViewModel>();
                foreach (var item in source.Images)
                {
                    destination.Images.Add(item.ToListItemViewModel());
                }
            }
            return destination;
        }
    }
}
