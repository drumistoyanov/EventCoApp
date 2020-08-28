using EventCoApp.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCo.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository Events { get; }
        int Complete();
    }
}
