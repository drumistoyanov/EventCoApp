using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventCoApp.Common.Enums
{
    public enum UserType
    {
        [Display(Name = "Master")]
        MASTER,
        [Display(Name = "Administrator")]
        ADMIN,
        [Display(Name = "Event_Creator")]
        EVENT_CREATOR,
        [Display(Name = "Normal_User")]
        NORMAL_USER      
    }
}
