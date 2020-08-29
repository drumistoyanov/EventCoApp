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
            var destination = new MessageViewModel();

            destination.EventId = source.EventId;
            destination.Content = source.Content;
            destination.CreatedBy = source.CreatedBy.UserName;

            return destination;
        }

        public static MessageListViewModel ToListItemViewModel(this Message source)
        {
            var destination = new MessageListViewModel();

            
            destination.EventId = source.EventId;
            destination.Content = source.Content;
            destination.CreatedBy = source.CreatedBy.UserName;
            destination.CreatedById = source.CreatedById.Value;
            destination.When = source.CreatedOn;

            return destination;
        }

        public static Message ToEntity(this MessageViewModel source)
        {
            var destination = new Message();

            destination.EventId = source.EventId;
            destination.Content = source.Content;
            destination.CreatedById = source.CreatedById;
            destination.CreatedOn = DateTime.Now;

            return destination;
        }

    }
}
