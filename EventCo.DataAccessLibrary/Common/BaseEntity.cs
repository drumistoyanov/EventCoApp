using EventCoApp.DataAccessLibrary.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventCoApp.DataAccessLibrary.Common
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreatedOn { get; set; }
#nullable enable
        public User? CreatedBy { get; set; }
        [ForeignKey("CreatedById")]
        public System.Nullable<int> CreatedById { get; set; }
#nullable disable
        public DateTime? ModifiedOn { get; set; }
#nullable enable
        public User? ModifiedBy { get; set; }
        [ForeignKey("ModifiedById")]
        public System.Nullable<int> ModifiedById { get; set; }
#nullable disable
        public bool IsDeleted { get; set; } = false;

    }
}
