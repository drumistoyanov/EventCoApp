using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.Common.Helpers
{
  public  class PermissionGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static List<PermissionGroup> GetList()
        {
            var groups = new List<PermissionGroup>() {
                new PermissionGroup() { Id = "news", Name = "News" },
                new PermissionGroup() { Id = "events", Name = "Events" },
                new PermissionGroup() { Id = "bookings", Name = "Bookings" },
                new PermissionGroup() { Id = "settings", Name = "Settings" }
            };

            return groups;
        }
    }
}
