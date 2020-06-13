using LazyGuy.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace LazyGuy.ClientDemo.DataAccess
{
    public abstract class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        // Note: 
        // from https://stackoverflow.com/questions/32282979/is-dbcontext-an-expensive-operation
        // and https://docs.microsoft.com/en-us/ef/ef6/fundamentals/performance/perf-whitepaper?redirectedfrom=MSDN#93-context-per-request
        // + Contexts are expected to be short lived and discarded   
        // + Generally speaking, one should avoid having a context instance throughout the life of the application, 
        //   as well as contexts per thread and static contexts.
        private readonly DbContext _dbContext;

        protected EFUnitOfWork()
        {
            _dbContext = GetDbContext();
        }

        #region IUnitOfWork

        public IRepository<T> GetRepository<T>() where T : class
        {
            var dbSet = _dbContext.Set<T>();
            return EFRepository<T>.Create(dbSet);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        #endregion

        #region IDisposable
        
        private bool isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            isDisposed = true;
        }

        #endregion

        protected abstract DbContext GetDbContext();

        public void SetEntityModified<T>(T entity, ICollection<string> fileds)
            where T : class
        {
            var entry = _dbContext.Entry(entity);
            foreach (var f in fileds)
            {
                entry.Property(f).IsModified = true;
            }
        }
    }
}
