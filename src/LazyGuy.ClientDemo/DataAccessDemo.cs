using LazyGuy.ClientDemo.DataAccess.Northwind;
using LazyGuy.ClientDemo.DataAccess.Northwind.Entities;
using System;

namespace LazyGuy.ClientDemo
{
    public class DataAccessDemo
    {
        public void ReadAllCategories()
        {
            using (var unit = new NorthwindUnitOfWork())
            {
                var all = unit.GetRepository<Category>().GetAll();
                foreach (var item in all)
                {
                    Console.WriteLine($"Id: {item.Id}, CategoryName: {item.CategoryName}, Description: {item.Description} ");
                }
            }
        }
    }
}
