using SchoolConnector.Models;
using System;

namespace DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
            {

                var std = new Student()
                {
                    Name = "John"
                };

                context.Students.Add(std);
                context.SaveChanges();
            }
        }
    }
}
