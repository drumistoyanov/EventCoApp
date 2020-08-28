using EventCoApp.DataAccessLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Image : BaseEntity
    {
        [Required]
        public string ImageData { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
