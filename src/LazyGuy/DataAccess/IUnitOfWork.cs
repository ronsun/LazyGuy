namespace LazyGuy.DataAccess
{
    /// <summary>
    /// Unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Save changes.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Get repository.
        /// </summary>
        /// <typeparam name="T">Type of repository.</typeparam>
        /// <returns>Repository.</returns>
        IRepository<T> GetRepository<T>()
            where T : class;
    }
}
