using EventCoApp.DataAccessLibrary.Models;
using System.Collections.Generic;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class Extensions
    {
        public static Role ToEntity(this RoleViewModel source)
        {
            Role destination = new Role
            {
                Name = source.Name
            };

            if (source.PermissionIds != null)
            {
                foreach (int permissionId in source.PermissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        PermissionId = permissionId
                    };

                    destination.RolePermissions.Add(rolePermission);
                }
            }

            return destination;
        }

        public static RoleViewModel ToViewModel(this Role source)
        {
            RoleViewModel destination = new RoleViewModel
            {
                Id = source.Id,
                Name = source.Name,
                ConcurrencyStamp = source.ConcurrencyStamp
            };

            if (source.RolePermissions != null)
            {
                foreach (RolePermission rolePermission in source.RolePermissions)
                {
                    destination.PermissionIds.Add(rolePermission.PermissionId);
                }
            }

            return destination;
        }

        public static List<RoleViewModel> ToListViewModel(this List<Role> source)
        {
            List<RoleViewModel> destination = new List<RoleViewModel>();

            if (source != null)
            {
                foreach (Role item in source)
                {
                    destination.Add(item.ToViewModel());
                }
            }

            return destination;
        }

        public static Role UpdateEntity(this RoleViewModel source, Role destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.ConcurrencyStamp = source.ConcurrencyStamp;

            List<RolePermission> oldPermissions = new List<RolePermission>();

            foreach (RolePermission rolePermission in destination.RolePermissions)
            {
                oldPermissions.Add(rolePermission);
            }

            foreach (RolePermission oldPermission in oldPermissions)
            {
                if (!source.PermissionIds.Contains(oldPermission.PermissionId))
                {
                    destination.RolePermissions.Remove(oldPermission);
                }
                else
                {
                    source.PermissionIds.Remove(oldPermission.PermissionId);
                }
            }

            if (source.PermissionIds != null)
            {
                foreach (int permissionId in source.PermissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        PermissionId = permissionId
                    };

                    destination.RolePermissions.Add(rolePermission);
                }
            }

            return destination;
        }
    }
}
