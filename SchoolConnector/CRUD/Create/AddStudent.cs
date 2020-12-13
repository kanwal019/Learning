using SchoolConnector.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolConnector.CRUD.Create
{
    public class AddStudent
    {
        public bool Add(string name)
        {
            try
            {
                using (var context = new SchoolContext())
                {
                    var std = new Student() { Name = name };
                    context.Students.Add(std);
                    context.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                _ = ex;
                return false;
            }              
        }
    }
}
