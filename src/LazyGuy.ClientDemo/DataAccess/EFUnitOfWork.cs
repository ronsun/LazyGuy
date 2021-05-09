using LazyGuy.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace LazyGuy.ClientDemo.DataAccess
{
    public abstract class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;
        private Dictionary<Type, dynamic> _repositories = new Dictionary<Type, dynamic>();

        protected EFUnitOfWork()
        {
            _dbContext = GetDbContext();
        }

        #region IUnitOfWork

        public IRepository<T> GetRepository<T>()
            where T : class
        {
            _repositories.TryGetValue(typeof(T), out var repository);
            if (repository == null)
            {
                var dbSet = _dbContext.Set<T>();
                repository = EFRepository<T>.Create(dbSet);
                _repositories.Add(typeof(T), repository);
            }

            return repository;
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
    }
}
