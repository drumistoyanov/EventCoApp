
using EventCoApp.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EventCoApp.DataAccessLibrary.Models
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Roles = new HashSet<IdentityUserRole<int>>();
            Logins = new HashSet<IdentityUserLogin<int>>();
            CreatedEvents = new HashSet<Event>();
            Messages = new HashSet<Message>();
            Bookings = new HashSet<Booking>();
            News = new HashSet<News>();
        }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }
        public UserType UserType { get; set; }

        public DateTime DateOfRegistration { get; set; }
        public bool IsSubscribed { get; set; } = false;
        public string ProfilePicture { get; set; }

        [JsonIgnore]
        public virtual ICollection<IdentityUserRole<int>> Roles { get; set; }
        [JsonIgnore]
        public virtual ICollection<IdentityUserLogin<int>> Logins { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; }
        [JsonIgnore]
        public virtual ICollection<Event> CreatedEvents { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual ICollection<News> News { get; set; }
    }
}
