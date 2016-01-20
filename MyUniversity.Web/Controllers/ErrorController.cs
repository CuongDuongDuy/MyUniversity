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
            Response.StatusCode = 400;
            return View("BadRequest");
        }

        public ActionResult InternalServerError()
        {
            Response.StatusCode = 500;
            return View("Error");
        }
    }
}