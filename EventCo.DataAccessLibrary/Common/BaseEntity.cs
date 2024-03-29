﻿using EventCoApp.DataAccessLibrary.Models;
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
        public int? CreatedById { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
