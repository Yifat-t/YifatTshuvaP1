using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YifatTshuvaP1.Models
{
    public class Teacher
    {
        //The following fields define a Teacher (Id, First name, Last name, Employee Number, 
        //Salary, Hire date, and Classes the teacher teach.
        public int TeacherId;
        public string TeacherFname;
        public string TeacherLname;
        public string EmployeeNumber;
        public decimal Salary;
        public DateTime HireDate;
        public IEnumerable<Class> ListOfClasses;  

      //  public Teacher() { }
    
    }
}