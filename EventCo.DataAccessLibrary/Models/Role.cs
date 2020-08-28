using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Role : IdentityRole<int>
    {
        public Role(string name) : base(name)
        {
            RolePermissions = new HashSet<RolePermission>();
        }
        public Role() : base()
        {
            RolePermissions = new HashSet<RolePermission>();
        }


        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
