using EventCoApp.DataAccessLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Message : BaseEntity
    {
        public string Content { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
