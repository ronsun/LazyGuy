using LazyGuy.DataAccess;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LazyGuy.ClientDemo.DataAccess
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private EFRepository() { }

        private DbSet<TEntity> _dbSet;

        public static EFRepository<TEntity> Create(DbSet<TEntity> dbSet)
        {
            var repository = new EFRepository<TEntity>
            {
                _dbSet = dbSet
            };

            return repository;
        }

        public IQueryable<TEntity> GetAll()
        {
            return Find(r => true);
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity Attach(TEntity entity)
        {
            return _dbSet.Attach(entity);
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }
    }
}
