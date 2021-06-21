using EventCoApp.DataAccessLibrary.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class News : BaseEntity
    {
        public News()
        {
            Images = new HashSet<NewsImage>();
        }
        [Required]
        [Column(TypeName = "nvarchar(1000)")]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<NewsImage> Images { get; set; }
        public bool Approved { get; set; } = false;
        public int Counter { get; set; } = 0;
        public string Name { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedById { get; set; }
    }
}
