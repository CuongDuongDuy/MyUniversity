using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyUniversity.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "My University MVC Web Application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Cuong Duong Duy.";

            return View();
        }
    }
}