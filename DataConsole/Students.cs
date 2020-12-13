using SchoolConnector.Classes;
using System;

namespace DataConsole
{
    class Students
    {
        //static void Main(string[] args)
        //{
        //    var student = new Student();
        //    AddStudents(student);
        //    FindStudents(student, "Smith");
        //}

        static void AddStudents(Student student)
        {
            student.Add("John Connor");
            student.Add("Jane Connor");
            student.Add("John Smith");
            student.Add("Jane Smith");
            student.Add("Jinx Smith");
        }

        static void FindStudents(Student student, string name)
        {
            var students = student.Find(name);
            if (students.Count > 0)
            {
                foreach (var stu in students)
                {
                    Console.WriteLine("Id - " + stu.StudentId);
                    Console.WriteLine("Name - " + stu.Name);
                    Console.WriteLine("Grade - " + stu.Grade);
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
