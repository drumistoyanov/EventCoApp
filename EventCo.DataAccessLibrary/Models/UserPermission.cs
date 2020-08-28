﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public partial class UserPermission
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public virtual User User { get; set; }
    }
}
