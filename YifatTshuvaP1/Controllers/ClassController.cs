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
    public class ClassController : Controller
    {

        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Class/List  
        public ActionResult List()
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses();
            return View(Classes);
        }

        //GET : /Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);


            return View(NewClass);
        }

    }
}

