using SchoolConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolConnector.Classes
{
    public class Student
    {
        private char GetDefaultGrade()
        {
            return 'B';
        }

        public bool Add(string name)
        {
            var student = new Models.Student()
            {
                Name = name,
                Grade = GetDefaultGrade()
            };

            return Add(student);
        }

        public bool Add(Models.Student student)
        {
            if (Get(student.Name).Count > 0)
            {
                return false;
            }

            try
            {
                using (var context = new SchoolContext())
                {
                    var std = new Models.Student() { 
                        Name = student.Name,
                        Grade = student.Grade
                    };
                    context.Students.Add(std);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ = ex;
                return false;
            }
        }

        public List<Models.Student> Find(string name)
        {
            try
            {
                var context = new SchoolContext();
                return context.Students.Where(s => s.Name.Contains(name)).ToList();      
            }
            catch (Exception ex)
            {
                _ = ex;
                return new List<Models.Student>();
            }
        }

        public List<Models.Student> Get(string name)
        {
            try
            {
                var context = new SchoolContext();
                return context.Students.Where(s => s.Name == name).ToList();
            }
            catch (Exception ex)
            {
                _ = ex;
                return new List<Models.Student>();
            }
        }
    }
}
