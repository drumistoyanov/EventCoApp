using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCoApp.WebApp.Models.Extensions
{
    public static partial class MessagesExtension
    {
        public static MessageViewModel ToViewModel(this Message source)
        {
            var destination = new MessageViewModel
            {
                EventId = source.EventId,
                Content = source.Content,
                CreatedBy = source.CreatedBy.UserName
            };

            return destination;
        }

        public static MessageListViewModel ToListItemViewModel(this Message source)
        {
            var destination = new MessageListViewModel
            {
                EventId = source.EventId,
                Content = source.Content
            };
            if (source.CreatedBy.ProfilePicture==null)
            {
                source.CreatedBy.ProfilePicture = "profilePicture.png";
            }
            destination.CreatedBy = source.CreatedBy;
            destination.CreatedById = source.CreatedById.Value;
            destination.When = source.CreatedOn;

            return destination;
        }

        public static Message ToEntity(this MessageViewModel source)
        {
            var destination = new Message
            {
                EventId = source.EventId,
                Content = source.Content,
                CreatedById = source.CreatedById,
                CreatedOn = DateTime.Now
            };

            return destination;
        }

    }
}
