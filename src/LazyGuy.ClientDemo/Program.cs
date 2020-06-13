using System;

namespace LazyGuy.ClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataAccessDemo = new DataAccessDemo();
            dataAccessDemo.ReadAllCategories();
            Console.ReadLine();
        }
    }
}
