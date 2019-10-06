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
        [Authorize]
        public FilePathResult DownloadFile(int? letterId)
        {
            var userId = WebSecurity.CurrentUserId;
            using (var db = new DataBaseContext())
            {
                var letter = db.Letters.FirstOrDefault(x => x.Id == letterId);
                if (letter != null)
                {
                    if ((letter.SendLetters.Any(x => x.OrderUser.Id == userId)) || (letter.IncomingLetters.Any(x => x.OrderUser.Id == userId)) || (letter.SendLetters.Any(x => x.OrderUser.Id == userId)))
                    {
                        if (letter.FilePuth != null)
                        {
                            return File(letter.FilePuth, letter.FileType, letter.FileName);
                        }
                    }
                }
            }
            return null;
        }

        [Authorize]
        public ActionResult OpenLetter(int? letterId, int? numberOfType)
        {
            var userId = WebSecurity.CurrentUserId;

            using (var db = new DataBaseContext())
            {
                if (numberOfType != null && numberOfType < 3)
                {
                    var typeOfLetter = db.listTypesOfLetter[(int)numberOfType].FirstOrDefault(x => x.Id == letterId && x.OrderUser.Id == userId);
                    if (typeOfLetter != null)
                    {
                        typeOfLetter.IsRead = true;
                        db.Entry(typeOfLetter).State = EntityState.Modified;
                        db.SaveChanges();
                        return View(typeOfLetter.Letter);
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteAllSelected(int[] arrayIdOfLetters, int? numberOfType, int? namberOfPeage)
        {

            var listLetters = new List<ITypesOfLetter>();

            if (arrayIdOfLetters != null)
            {
                var userId = WebSecurity.CurrentUserId;
                using (var db = new DataBaseContext())
                {
                    if (numberOfType != null && numberOfType < 3)
                    {
                        var listTypeOfLetter = (db.listTypesOfLetter[(int)numberOfType].Join(arrayIdOfLetters, x => x.Id, y => y, (x, y) => x).Where(x => x.OrderUser.Id == userId)).Include(x => x.Letter);
                        if (listTypeOfLetter != null)
                        {
                            foreach (var typeOfLetter in listTypeOfLetter)
                            {
                                typeOfLetter.Letter.NumberOfOwners--;

                                if (typeOfLetter.Letter.NumberOfOwners == 0)
                                {
                                    db.Entry(typeOfLetter.Letter).State = EntityState.Deleted;
                                }
                                else
                                {
                                    db.Entry(typeOfLetter.Letter).State = EntityState.Modified;
                                }
                                db.Entry(typeOfLetter).State = EntityState.Deleted;
                            }
                        }
                        listLetters.AddRange(listTypeOfLetter);
                    }
                    db.SaveChanges();
                }
            }
            return Content(CustomHelperMetods.MyGrid(listLetters, namberOfPeage.ToString()));
        }


        [Authorize]
        public ActionResult DeleteLetter(int? letterId, int? numberOfType, int? namberOfPeage)
        {
            var userId = WebSecurity.CurrentUserId;
            using (var db = new DataBaseContext())
            {
                if (numberOfType != null && numberOfType < 3)
                {
                    var typeLetter = db.listTypesOfLetter[(int)numberOfType].FirstOrDefault(x => x.Letter.Id == letterId || x.OrderUser.Id == userId);
                    if (typeLetter != null)
                    {
                        if (typeLetter.Letter.NumberOfOwners == 0)
                        {
                            db.Entry(typeLetter.Letter).State = EntityState.Deleted;
                        }
                        else
                        {
                            db.Entry(typeLetter.Letter).State = EntityState.Modified;
                        }
                        db.Entry(typeLetter).State = EntityState.Deleted;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("ShowTypesLetters", "Message", new { numberOfType, namberOfPeage });
        }

        [Authorize]
        public ActionResult New_letter()
        {
            ViewBag.user = User.Identity.Name;
            return View(new Letter());
        }

        [Authorize]
        public ActionResult AnswerLetter(string fromWhom)
        {
            var newLetter = new Letter();
            newLetter.ToWhom = fromWhom;
            return View("New_letter", newLetter);
        }
        [Authorize]
        public ActionResult ForwardLetter(int? letterId)
        {
            var letter = new Letter();
            var userName = User.Identity.Name;
            ViewBag.user = userName;
            using (var db = new DataBaseContext())
            {
                letter = db.Letters.FirstOrDefault(x => x.Id == letterId);
                if (letter.Id != 0)
                {
                    if (userName.Equals(letter.FromWhom))
                    {
                        return View("New_letter", letter);
                    }
                    else
                    {
                        var listRecipients = GetAllRecipients(letter);
                        foreach (var resipient in listRecipients)
                        {
                            if (userName.Equals(resipient))
                            {
                                return View("New_letter", letter);
                            }
                        }
                    }
                }

            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult New_letter(Letter letter, HttpPostedFileBase upload, int? letterId)
        {
            using (var db = new DataBaseContext())
            {
                var listAllRecidients = GetAllRecipients(letter);

                var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);
                db.Letters.Add(CreateNewLetter(letter, user, listAllRecidients.Count, upload));


                if (user.SendLetters.Count >= UserProfile.MaxSendLetters)
                {
                    var oldestSendLetter=user.SendLetters.First(x => x.Data == user.SendLetters.Min(y => y.Data));
                    if (--oldestSendLetter.Letter.NumberOfOwners == 0)
                    {
                        db.Entry(oldestSendLetter.Letter).State = EntityState.Deleted;
                    }
                    db.Entry(oldestSendLetter).State = EntityState.Deleted;
                    db.Entry(user.IncomingLetters.Min(x=>x.Letter.Date)).State = EntityState.Added;
                }
                else
                {
                    db.Entry(CreateTypeOfLetter<SendLetter>(letter, user, letter.ToWhom)).State = EntityState.Added;
                }
                



                foreach (var resipient in (db.UserProfiles.Join(listAllRecidients, x => x.UserMail, y => y, (x, y) => x).Include(x => x.SpamMeils)))
                {
                    var send = false;
                    foreach (var spamMail in resipient.SpamMeils)
                    {
                        if (spamMail.ToWhomMail == user.UserMail)
                        {
                            db.Entry(CreateTypeOfLetter<SpamLetter>(letter, resipient, user.UserMail)).State = EntityState.Added;
                            send = true;
                            break;
                        }
                    }
                    if (send != true)
                    {
                        db.Entry(CreateTypeOfLetter<IncomingLetter>(letter, resipient, user.UserMail)).State = EntityState.Added;
                    }
                }

               

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
        public ActionResult ShowTypesLetters(int? numberOfType, int? namberOfPeage)
        {
            var currentUserId = WebSecurity.CurrentUserId;
            var listLetters = new List<ITypesOfLetter>();
            if (numberOfType != null)
            {
                using (var db = new DataBaseContext())
                {
                    if (numberOfType != null && numberOfType < 3)
                    {
                        listLetters = db.listTypesOfLetter[(int)numberOfType].Where(x => x.OrderUser.Id == currentUserId).OrderBy(x => x.Letter.Date).ToList();

                        ViewBag.Title = numberOfType == (int)NumberOfTypes.IncomingLetters ? "Incoming Letters" : numberOfType ==
                                                               (int)NumberOfTypes.SendLetters ? "Send Letters" : "Spam letters";
                    }
                }
            }
            if (listLetters.Count >= 1)
            {
                ViewBag.NumberOfType = numberOfType;
                ViewBag.NamberOfPeage = namberOfPeage == null ? 1 : namberOfPeage;
                return View(listLetters);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ShowTypesLetters(int[] a, int? numberOfType, int? namberOfPeage)
        {
            ViewBag.NumberOfType = numberOfType;
            ViewBag.NamberOfPeage = namberOfPeage == null ? 1 : namberOfPeage;
            return View();
        }



        public Letter CreateNewLetter(Letter letter, UserProfile user, int allRecipientsCount, HttpPostedFileBase upload)
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

                letter.FilePuth = filePuth;
                letter.FileType = upload.ContentType;
                letter.FileName = upload.FileName;
            }
            letter.Date = DateTime.Now;
            letter.FromWhom = user.UserMail;
            letter.NumberOfOwners = allRecipientsCount + 1; //"+1" This is the user who sent the lette

            return letter;
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
    }
}