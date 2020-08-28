using EventCoApp.DataAccessLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Booking:BaseEntity
    {
        public int EventId { get; set; }
        public  Event Event { get; set; }
        public int TicketCount { get; set; }
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal WholePrice { get; set; }
    }
}
