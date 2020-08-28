using EventCoApp.DataAccessLibrary.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Ticket : BaseEntity
    {
        [Required]
        [Range(typeof(decimal), "0.0", "79228162514264337593543950335")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
