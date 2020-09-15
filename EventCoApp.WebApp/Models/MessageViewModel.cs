using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class MessageViewModel
    {
        public string Content { get; set; }
        public int EventId { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedById { get; set; }
    }

    public class MessageListViewModel
    {

        [Display(Name = "Message Content")]
        public string Content { get; set; }

        [Display(Name = "Event ID")]
        public int EventId { get; set; }

        [Display(Name = "Created By")]
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public DateTime When { get; set; }
    }
}
