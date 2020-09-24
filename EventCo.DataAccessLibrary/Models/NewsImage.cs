using EventCoApp.DataAccessLibrary.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class NewsImage : BaseEntity
    {
        [Required]
        public string ImageData { get; set; }
        public int NewsId { get; set; }
        public News News { get; set; }
    }
}
