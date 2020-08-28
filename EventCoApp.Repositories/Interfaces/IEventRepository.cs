using EventCoApp.DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.Core.Repositories.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        Event GetTopAttendantEvent();
        IEnumerable<Event> GetTopSellingEvents(int count);
        IEnumerable<Event> GetEventsWithOrganiser(User organiser);
        IEnumerable<Event> GetEventsWithLocation(string location);
    }
}
