using System;
using System.Threading;

namespace LazyGuy.ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAccessDemo = new DataAccessDemo();
            dataAccessDemo.ReadAllCategories();
            dataAccessDemo.UpdateCategoryDemo();
            Console.ReadLine();
        }
    }
}
