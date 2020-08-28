using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models
{
    public class BookingViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime RequestDate { get; set; }
        [Range(typeof(int), "0", "100000")]
        public int TicketCountLeft { get; set; }
        [Range(1, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int TicketCount { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WholePrice { get; set; }
        public EventDetailsViewModel Event { get; set; }
    }

    public class BookingListItemViewModel
    {
        [Display(Name = "Booking Id")]
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime RequestDate { get; set; }
        public int TicketCount { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WholePrice { get; set; }
        public EventDetailsViewModel Event { get; set; }
    }

    public class BookingDetailsViewModel
    {
        public int Location { get; set; }
        public DateTime When { get; set; }
        public int TicketCount { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WholePrice { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TicketPrice { get; set; }
        public EventDetailsViewModel Event { get; set; }
    }
}
