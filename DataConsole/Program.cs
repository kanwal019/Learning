using SchoolConnector.CRUD.Create;
using SchoolConnector.Models;
using System;

namespace DataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var addStudent = new AddStudent();
            addStudent.Add("Smith");
            addStudent.Add("Jane");
        }
    }
}
