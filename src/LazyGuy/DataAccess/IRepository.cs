using System;
using System.Linq;
using System.Linq.Expressions;

namespace LazyGuy.DataAccess
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        T Add(T entity);

        T Delete(T entity);
    }
}
