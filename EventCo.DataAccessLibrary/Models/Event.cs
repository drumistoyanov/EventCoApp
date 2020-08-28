using EventCoApp.Common.Enums;
using EventCoApp.DataAccessLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Event : BaseEntity
    {
        public Event()
        {
            Images = new List<Image>();
            Tickets = new List<Ticket>();
        }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public DateTime When { get; set; }
        public int TicketCount { get; set; } = 100;
        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; } = 0;
        public int Counter { get; set; } = 0;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
