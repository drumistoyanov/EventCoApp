using AutoMapper;
using EventCoApp.DataAccessLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCoApp.Services
{
    public abstract class BaseEFService
    {
        protected BaseEFService(
            EventCoContext dbContext,
            IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        protected EventCoContext DbContext { get; private set; }

        protected IMapper Mapper { get; private set; }
    }
}
