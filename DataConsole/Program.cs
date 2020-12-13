using SchoolConnector.Classes;
using System;

namespace DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student();
            student.Add("John Connor");
            student.Add("Jane Connor");
            student.Add("John Smith");
            student.Add("Jane Smith");

            var students = student.Get("Smith");
            if (students.Count > 0)
            {
                foreach(var stu in students)
                {
                    Console.WriteLine("Id - " + stu.StudentId);
                    Console.WriteLine("Name - " + stu.Name);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Student with name \" Smith \" - Not Found !!");
            }

        }
    }
}
