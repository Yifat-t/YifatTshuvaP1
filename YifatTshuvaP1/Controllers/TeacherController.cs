using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using YifatTshuvaP1.Models;
using System.Diagnostics;

namespace YifatTshuvaP1.Controllers
{
    public class TeacherController : Controller
    {
        //This controller class will listen for HTTP requests and provide a response to the dynamically rendered view. 
        // GET: Teacher
        //here we are linking a dynamic rendered page to the controller
        public ActionResult Index()
        {
            return View();
        }



        //GET : /Teacher/List  
        //GET : /Api/TeacherData/ListTeachers/{SearchKey}
        

        public ActionResult List(String SearchKey=null)
        {


            Debug.WriteLine("The inputted search key is");
            Debug.WriteLine(SearchKey);
            //connecting teacher contrller to the web api controller which access the data.
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        //here we are linking another dynamic rendered page to the controller
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            if (NewTeacher != null)
            {
                ClassDataController classDataController = new ClassDataController();
                IEnumerable<Class> listOfTeacherClasses = classDataController.ListTeachersClasses(id);

                NewTeacher.ListOfClasses = listOfTeacherClasses;
            }

            return View(NewTeacher);
        }

    }
}
