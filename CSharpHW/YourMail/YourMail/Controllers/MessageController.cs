using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourMail.Models;

namespace YourMail.Controllers
{
    public class MessageController : Controller
    {

        [Authorize]
        public ActionResult New_letter()
        {
            ViewBag.user = User.Identity.Name;
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult New_letter(Letter letter)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}