using EventCoApp.Common.Enums;
using EventCoApp.DataAccessLibrary.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class Log : BaseEntity
    {
        [Required]
        public LogType Type { get; set; }
#nullable enable
        [Column(TypeName = "nvarchar(1000)")]
        public string? Description { get; set; }
        public int? EventId { get; set; }
        public string? UserId { get; set; }
        public int? NewsId { get; set; }
#nullable disable
        public int SiteCounter { get; set; }
    }
}
