using EventCoApp.Core.Repositories.Interfaces;
using EventCoApp.DataAccessLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EventCoApp.Core.Repositories
{
    public class Repository<TEntity>:IRepository<TEntity> where TEntity:class
    {
        protected readonly EventCoContext context;

        public Repository(EventCoContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.context.Set<TEntity>().AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.context.Set<TEntity>().Where(predicate);
        }

        public TEntity Get(Guid id)
        {
            return this.context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public void Remove(TEntity entity)
        {
             this.context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
             this.context.Set<TEntity>().RemoveRange(entities);
        }
    }
}
