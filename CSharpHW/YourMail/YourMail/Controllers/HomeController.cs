using System.Web.Mvc;

namespace MvcEmpty.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize] 
        public ActionResult Private()
        {
            ViewBag.Message = "Private Personal Page.";

            return View();
        }

    }
}