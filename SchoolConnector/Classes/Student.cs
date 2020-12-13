using SchoolConnector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchoolConnector.Classes
{
    public class Student
    {
        public bool Add(string name)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var std = new Models.Student() { Name = name };
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

        public List<Models.Student> Get(string name)
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
    }
}
