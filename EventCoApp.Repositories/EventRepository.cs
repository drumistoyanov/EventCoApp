using EventCoApp.Common.BindingModels.Event;
using EventCoApp.Core.Repositories.Interfaces;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCoApp.Core.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(EventCoContext context)
            : base(context)
        {

        }
        public IEnumerable<Event> GetEventsWithLocation(string location)
        {
            return context.Events.Where(e => e.Location.Name == location);
        }

        public IEnumerable<Event> GetEventsWithOrganiser(User organiser)
        {
            return context.Events.Where(e => e.CreatedBy == organiser);
        }

        public Event GetTopAttendantEvent()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Event> GetTopSellingEvents(int count)
        {
            throw new NotImplementedException();
        }

        //public Event GetTopAttendantEvent()
        //{
        //    IEnumerable<EventTickets> eventTickets = context.Events
        //        .Include(e => e.Tickets)
        //        .Select(x => new EventTickets()
        //        {
        //            EventID = x.ID,
        //            TicketCount = x.Tickets.Sum(t => t.Count)
        //        }).ToList();
        //    var eventID = eventTickets.OrderBy(et => et.TicketCount).FirstOrDefault().EventID;
        //    return context.Events.FirstOrDefault(e => e.ID == eventID);


        //}

        //public IEnumerable<Event> GetTopSellingEvents(int count)
        //{
        //    IEnumerable<EventTickets> eventTickets = context.Events
        //        .Include(e => e.Tickets)
        //        .Select(x => new EventTickets()
        //        {
        //            EventID = x.ID,
        //            TicketCount = x.Tickets.Sum(t => t.Count)
        //        }).ToList().Take(count);
        //    IEnumerable<int> eventsIds = eventTickets.Select(x => x.EventID);

        //    return context.Events.Where(x => eventsIds.Contains(x.ID));
        //}
    }
}
