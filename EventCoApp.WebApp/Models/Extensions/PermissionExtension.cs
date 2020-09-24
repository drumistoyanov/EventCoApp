using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.WebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static PermissionViewModel ToViewModel(this Permission source)
        {
            var destination = new PermissionViewModel
            {
                Id = source.Id,
                Name = source.Name,
                Group = source.Group,
                Description = source.Description
            };

            return destination;
        }
    }

    public static partial class Extensions
    {
        public static List<PermissionViewModel> ToListViewModel(this List<Permission> source)
        {
            var destination = new List<PermissionViewModel>();

            if(source != null)
            {
                foreach(var item in source)
                {
                    destination.Add(item.ToViewModel());
                }
            }

            return destination;
        }
    }
}
