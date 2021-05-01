using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
using System.Web.Mvc;
using YifatTshuvaP1.Models;
using System.Diagnostics;//for debugging the new method if neede

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
            Teacher SelectedTeacher = controller.FindTeacher(id);

            if (SelectedTeacher != null)
            {
                ClassDataController classDataController = new ClassDataController();
                IEnumerable<Class> listOfTeacherClasses = classDataController.ListTeachersClasses(id);

                SelectedTeacher.ListOfClasses = listOfTeacherClasses;
            }

            return View(SelectedTeacher);
        }


        //GET : /Teacher/DeleteCofirm/{id}
        //Creating a method to allow us to delete a record, 
        // we are still finding and displaying teacher in our actual view
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

        //this method will be responsible to receive the post request for deleting teacher/s record by id
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        ///refering the action to list of teachers
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");         
        }   
        //Get : /Teacher/New
        //This time we are not adding id because when we creating new record we are not required to 
        //provide this information.
        public ActionResult New()
        {
            return View();
        }
        //POST : //Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, decimal Salary)
        {

            //we need to validate that the Create method is working  and receiving the inputs from the form

            Debug.WriteLine("Create Method is working");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(Salary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.Salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        //Get : /Teacher/Update/{id} 
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        public ActionResult Ajax_Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher SelectedTeacher = controller.FindTeacher(id);

            return View(SelectedTeacher);
        }
        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="Salary">The updated salary of the teacher.</param>

        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/id
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFname":"Yifat",
        ///	"TeacherLname":"Tshuva",
        ///	"TeacherSalary":"123",
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname,  decimal Salary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.Salary = Salary;
            

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}
