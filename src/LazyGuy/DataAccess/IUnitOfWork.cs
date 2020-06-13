namespace LazyGuy.DataAccess
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        IRepository<T> GetRepository<T>() where T : class;
    }
}
