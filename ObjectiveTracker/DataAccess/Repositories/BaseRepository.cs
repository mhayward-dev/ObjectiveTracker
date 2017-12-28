using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ObjectiveTracker.DataAccess.Repositories
{
    public class BaseRepository<T, Repository>
          where T : class
          where Repository : class
    {
        public ObjectiveTrackerContext Context { get; private set; }
        protected Repository CurrentRepository { get; set; }
        public IQueryable<T> Query { get; protected set; }

        public BaseRepository(DbContext context)
        {
            Context = (ObjectiveTrackerContext)context;
        }

        public virtual Repository EagerLoad()
        {
            return CurrentRepository;
        }

        public virtual T Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual Repository All()
        {
            Query = Context.Set<T>();

            return CurrentRepository;
        }

        public virtual Repository For(Expression<Func<T, bool>> filter)
        {
            Query = Query.Where(filter);

            return CurrentRepository;
        }

        public virtual T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return Context.Set<T>().FindAsync(id);
        }

        public virtual IEnumerable<T> Results()
        {
            return Query.AsEnumerable();
        }

        public virtual Task<IEnumerable<T>> ResultsAsync()
        {
            return Task.FromResult(Query.AsEnumerable());
        }

        public virtual Task<int> UpdateAsync()
        {
            return Context.SaveChangesAsync();
        }
    }
}
