using EventCoApp.DataAccessLibrary.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class News : BaseEntity
    {
        public News()
        {
            Images = new HashSet<Image>();
        }
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }
        public virtual  ICollection<Image> Images { get; set; }
    }
}
