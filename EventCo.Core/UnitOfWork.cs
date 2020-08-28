using EventCo.Core.Interfaces;
using EventCoApp.Core.Repositories;
using EventCoApp.Core.Repositories.Interfaces;
using EventCoApp.DataAccessLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCo.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventCoContext _context;
        public UnitOfWork(EventCoContext context)
        {
            _context = context;
            Events = new EventRepository(_context);
        }
        public IEventRepository Events { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
