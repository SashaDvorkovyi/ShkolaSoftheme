using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using YourMail.Models;
using YourMail.Interfaces;
using System.Web.Security;
using YourMail.Infrastructure;

namespace YourMail.Controllers
{
    public class MessageController : Controller
    {
        [HttpPost]
        [Authorize]
        public ActionResult OpenLetter(int? id)
        {
            var user = WebSecurity.CurrentUserId;
            using(var db = new DataBaseContext())
            {

            }
            return RedirectToAction("AddSpamMail", "Message");
        }

        [Authorize]
        public ActionResult New_letter()
        {
            ViewBag.user = User.Identity.Name;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult New_letter(Letter letter, HttpPostedFileBase upload)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);

                db.Entry(CreateTypeOfLetter<SendLetter>(letter, user, letter.ToWhom)).State = EntityState.Added;

                db.Entry(ChengeUser<SendLetter>(user)).State = EntityState.Modified;

                foreach (var resipient in (db.UserProfiles.Join(GetAllRecipients(letter), x => x.UserMail, y => y, (x, y) => x).Include(x => x.SpamMeils)))
                {
                    var send = false;
                    foreach(var spamMail in resipient.SpamMeils)
                    {
                        if (spamMail.ToWhomMail == user.UserMail)
                        {
                            db.Entry(CreateTypeOfLetter<SpamLetter>(letter, resipient, user.UserMail)).State = EntityState.Added;
                            db.Entry(ChengeUser<SpamLetter>(resipient)).State = EntityState.Modified;
                            send = true;
                            break;
                        }
                    }
                    if (send != true)
                    {
                        db.Entry(CreateTypeOfLetter<IncomingLetter>(letter, resipient, user.UserMail)).State = EntityState.Added;
                        db.Entry(ChengeUser<IncomingLetter>(resipient)).State = EntityState.Modified;
                    }
                }

                db.Letters.Add(CreateNewLetter(letter, user, GetAllRecipients(letter), upload));

                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult AddSpamMail()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddSpamMail(SpamMeil spamMeil)
        {
            using (var db = new DataBaseContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);

                spamMeil.OrderUserId = user.Id;

                db.Entry(spamMeil).State = EntityState.Added;

                db.SaveChanges();
            }

            return RedirectToAction("AddSpamMail", "Message");
        }

        [Authorize]
        public ActionResult ShowTypesLetters(int? numberOfType)
        {
            var currentUserId = WebSecurity.CurrentUserId;
            var listLetters = new List<ITypesOfLetter>();
            if (numberOfType != null)
            {
                using (var db = new DataBaseContext())
                {
                    if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                    {
                        var listIncomingLetters = db.IncomingLetters.Where(x => x.OrderUserId == currentUserId).OrderBy(x => x.Date).ToList();
                        foreach(var incomingLetter in listIncomingLetters)
                        {
                            listLetters.Add(incomingLetter);
                        }
                        ViewBag.Title = "Incoming letters";
                    }
                    else if (numberOfType == (int)NumberOfTypes.SendLetters)
                    {
                        var list = db.SendLetters.Where(x => x.OrderUserId == currentUserId).OrderBy(x => x.Date).ToList();
                        foreach(var item in list)
                        {
                            listLetters.Add(item);
                        }
                        ViewBag.Title = "Send letters";
                    }
                    else if (numberOfType == (int)NumberOfTypes.SpamLetters)
                    {
                        var list = db.SpamLetters.Where(x => x.OrderUserId == currentUserId).OrderBy(x => x.Date).ToList();
                        foreach (var item in list)
                        {
                            listLetters.Add(item);
                        }
                        ViewBag.Title = "Spam letters";
                    }
                }
            }
            if (listLetters.Count >= 1)
            {
                ViewBag.NumberOfType = numberOfType;
                return View(listLetters);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ShowTypesLetters(int[] a, int? numberOfType)
        {
            ViewBag.NumberOfType = numberOfType;
            return View();
        }

        public Letter CreateNewLetter(Letter letter, UserProfile user, List<string> allRecipients, HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                var filePuth = "~/Files/" + Guid.NewGuid().ToString();
                try
                {
                    upload.SaveAs(Server.MapPath(filePuth));
                }
                catch (Exception err)
                {
                    return new Letter();
                }
                letter.FromWhom = user.UserMail;
                letter.NumberOfOwners = allRecipients.Count + 1; //"+1" This is the user who sent the letter
                letter.FilePuth = filePuth;
                letter.FileType = upload.ContentType;
                letter.FileName = upload.FileName;

            }
            return letter;
        }

        public T CreateTypeOfLetter<T>(Letter letter, UserProfile userOrder, string userToOrFromWhomMail) where T : class, ITypesOfLetter, new()
        {
                var tLetter = new T();

                tLetter.Subject = letter.Subject;

                tLetter.ToOrFromWhomMail = userToOrFromWhomMail;

                tLetter.Date = letter.Date;

                tLetter.OrderUserId = userOrder.Id;

                tLetter.LetterId = letter.Id;

                return tLetter;
        }

        public List<string> GetAllRecipients(Letter letter)
        {
            var listRecipientPerson = new List<string>();
            var mail = default(string);
            foreach (char x in letter.ToWhom)
            {
                if (x == ' ' && mail != null)
                {
                    if (mail[mail.Length - 1] == ',' || mail[mail.Length - 1] == ';')
                    {
                        listRecipientPerson.Add(mail.Substring(0, mail.Length - 1));
                        mail = default(string);
                    }
                    else
                    {
                        listRecipientPerson.Add(mail);
                        mail = default(string);
                    }
                }
                else
                {
                    mail += x;
                }
            }
            if (mail != null)
            {
                listRecipientPerson.Add(mail);
                mail = default(string);
            }
            return listRecipientPerson = listRecipientPerson.Distinct().ToList(); //delete all repetitions
        }

        public UserProfile ChengeUser<T>( UserProfile userOrder) where T : class, ITypesOfLetter, new()
        {
            if (new T() is IncomingLetter)
            {
                userOrder.CountAllIncomingLetters = userOrder.CountAllIncomingLetters == null ? 1 : userOrder.CountAllIncomingLetters + 1;
                userOrder.CountDontReadIncomingLetters = userOrder.CountDontReadIncomingLetters == null ? 1 : userOrder.CountDontReadIncomingLetters + 1;
                return userOrder;
            }
            else if (new T() is SendLetter)
            {
                userOrder.CountAllSendLetters = userOrder.CountAllSendLetters == null ? 1 : userOrder.CountAllSendLetters + 1;
                return userOrder;
            }
            else if (new T() is SpamLetter)
            {
                userOrder.CountAllSpamLetters = userOrder.CountAllSpamLetters == null ? 1 : userOrder.CountAllSpamLetters + 1;
                userOrder.CountDontReadSpamLetters = userOrder.CountDontReadSpamLetters == null ? 1 : userOrder.CountDontReadSpamLetters + 1;
                return userOrder;
            }
            else
            {
                return null;
            }
        }
    }
}