using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using YifatTshuvaP1.Models;


namespace YifatTshuvaP1.Controllers
{
    public class StudentController : Controller
    {

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }





        //GET : /Student/List  
        //GET : /Api/StudentData/ListStudents/{SearchKey}

        public ActionResult List(String SearchKey = null)
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents(SearchKey);
            return View(Students);
        }

        //GET : /Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);

            if (NewStudent != null)
            {
                //ClassDataController classDataController = new ClassDataController();
                //IEnumerable<Class> listOfStudentClasses = classDataController.ListStudentsClasses(id);

                //NewStudent.ListOfClasses = listOfStudentClasses;
            }

            return View(NewStudent);
        }

    }
}
