using LazyGuy.ClientDemo.DataAccess;
using LazyGuy.ClientDemo.DataAccess.Northwind;
using LazyGuy.ClientDemo.DataAccess.Northwind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public void UpdateCategoryDemo()
        {
            Console.WriteLine($"===== {nameof(UpdateCategoryDemo)} =====");
            using (var unit = new NorthwindUnitOfWork())
            {
                var repo = unit.GetRepository<Category>();
                var before = repo.Find(r => r.Id == 1).First();
                Console.WriteLine($"Before => Id: {before.Id}, CategoryName: {before.CategoryName}, Description: {before.Description} ");

                before.Description = DateTime.Now.ToString();
                unit.SaveChanges();

                var after = unit.GetRepository<Category>().Find(r => r.Id == 1).First();
                Console.WriteLine($"After => Id: {after.Id}, CategoryName: {after.CategoryName}, Description: {after.Description} ");
            }
        }
    }
}
