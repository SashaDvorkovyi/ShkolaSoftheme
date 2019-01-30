using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
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
            using(var db= new DataBaseContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);

                letter.FromWhom = user.UserMail;

                db.Letters.Add(letter);

                var incomingLetter = new IncomingLetter(user.Id);

                if (user.CountAllIncomingLetters < UserProfile.MaxIncomingLetters)
                {
                    incomingLetter = db.IncomingLetters.FirstOrDefault(x => x.IsExist == false);
                }
                else
                {
                    incomingLetter = db.IncomingLetters.OrderByDescending(x => x.Date).First();
                }
                incomingLetter
            }
            return RedirectToAction("Index", "Home");
        }
    }
}