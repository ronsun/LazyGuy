using LazyGuy.ClientDemo.DataAccess.Northwind.Entities;
using System.Data.Entity;

namespace LazyGuy.ClientDemo.DataAccess.Northwind
{
    public class NorthwindUnitOfWork : EFUnitOfWork
    {
        protected override DbContext GetDbContext()
        {
            return new NorthwindContext();
        }

        private class NorthwindContext : DbContext
        {
            public NorthwindContext()
                : base("name=NorthwindConnection")
            {
            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Category>().ToTable("Category");
            }
        }
    }
}
