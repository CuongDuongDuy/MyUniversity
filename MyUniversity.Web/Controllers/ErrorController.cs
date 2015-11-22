using System.Web.Mvc;

namespace MyUniversity.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        public ActionResult BadRequest()
        {
            Response.StatusCode = 500;
            return View("BadRequest");
        }
    }
}