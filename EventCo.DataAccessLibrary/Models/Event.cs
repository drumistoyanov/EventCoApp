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
            Images = new HashSet<Image>();
            Tickets = new HashSet<Ticket>();
            Messages = new HashSet<Message>();
            Bookings = new HashSet<Booking>();
        }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(max)")]
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
#nullable disable
        public DateTime? ModifiedOn { get; set; }
#nullable enable
        public User? ModifiedBy { get; set; }
        [ForeignKey("ModifiedById")]
        public int? ModifiedById { get; set; }
#nullable disable
        public int Counter { get; set; } = 0;
        public bool VisibleChat { get; set; } = true;
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
