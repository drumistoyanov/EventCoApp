using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public partial class Location
    {
        public Location()
        {
            Events = new HashSet<Event>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
