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

        public FilePathResult DownloadFile(int? letterId)
        {
            var userId = WebSecurity.CurrentUserId;
            using (var db = new DataBaseContext())
            {
                var letter = db.Letters.FirstOrDefault(x => x.Id == letterId);
                if (letter != null)
                {
                    if ((letter.SendLetters.Any(x => x.OrderUserId == userId)) || (letter.IncomingLetters.Any(x => x.OrderUserId == userId)) || (letter.SendLetters.Any(x => x.OrderUserId == userId)))
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
            var correctUser = false;
            using (var db = new DataBaseContext())
            {
                var letter = db.Letters.FirstOrDefault(x => x.Id == letterId);
                if (letter != null)
                {
                    if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                    {
                       var incomingLetters = letter.IncomingLetters.FirstOrDefault(x => x.OrderUserId == userId);
                        if (incomingLetters.Id!=0)
                        {
                            incomingLetters.IsRead = true;
                            db.Entry(incomingLetters).State = EntityState.Modified;
                            correctUser = true;
                        }
                    }
                    else if (numberOfType == (int)NumberOfTypes.SendLetters)
                    {
                        var sendLetters = letter.SendLetters.FirstOrDefault(x => x.OrderUserId == userId);
                        if (sendLetters.Id!=0)
                        {
                            sendLetters.IsRead = true;
                            db.Entry(sendLetters).State = EntityState.Modified;
                            correctUser = true;
                        }
                    }
                    else if (numberOfType == (int)NumberOfTypes.SpamLetters)
                    {
                        var spamLetters = letter.SpamLetters.FirstOrDefault(x => x.OrderUserId == userId);
                        if (spamLetters!=null)
                        {
                            spamLetters.IsRead = true;
                            db.Entry(spamLetters).State = EntityState.Modified;
                            correctUser = true;
                        }
                    }
                    db.SaveChanges();
                }
                if (correctUser)
                {

                    return View(letter);
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
                    if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                    {
                        var listIncomingLetters = (db.IncomingLetters.Join(arrayIdOfLetters, x => x.Id, y => y, (x, y) => x).Where(x => x.OrderUserId == userId)).Include(x=>x.Letter);
                        if (listIncomingLetters != null)
                        {
                            foreach (var incomingLetter in listIncomingLetters)
                            {
                                incomingLetter.Letter.NumberOfOwners--;
                                if (incomingLetter.Letter.NumberOfOwners == 0)
                                {
                                    db.Entry(incomingLetter.Letter).State = EntityState.Deleted;
                                }
                                else
                                {
                                    db.Entry(incomingLetter.Letter).State = EntityState.Modified;
                                }
                                db.Entry(incomingLetter).State = EntityState.Deleted;
                            }
                        }
                        listLetters.AddRange(listIncomingLetters);
                    }
                    else if (numberOfType == (int)NumberOfTypes.SendLetters)
                    {
                        var listSendLetters = (db.SendLetters.Join(arrayIdOfLetters, x => x.Id, y => y, (x, y) => x).Where(x => x.OrderUserId == userId)).Include(x=>x.Letter);
                        if (listSendLetters != null)
                        {
                            foreach (var sendLetter in listSendLetters)
                            {
                                sendLetter.Letter.NumberOfOwners--;
                                if (sendLetter.Letter.NumberOfOwners == 0)
                                {
                                    db.Entry(sendLetter.Letter).State = EntityState.Deleted;
                                }
                                else
                                {
                                    db.Entry(sendLetter.Letter).State = EntityState.Modified;
                                }
                                db.Entry(sendLetter).State = EntityState.Deleted;
                            }
                        }
                        listLetters.AddRange(listSendLetters);
                    }
                    else if (numberOfType == (int)NumberOfTypes.SpamLetters)
                    {
                        var listSpamLetters = (db.SpamLetters.Join(arrayIdOfLetters, x => x.Id, y => y, (x, y) => x).Where(x => x.OrderUserId == userId)).Include(x => x.Letter);
                        if (listSpamLetters != null)
                        {
                            foreach (var spamLetter in listSpamLetters)
                            {
                                spamLetter.Letter.NumberOfOwners--;
                                if (spamLetter.Letter.NumberOfOwners == 0)
                                {
                                    db.Entry(spamLetter.Letter).State = EntityState.Deleted;
                                }
                                else
                                {
                                    db.Entry(spamLetter.Letter).State = EntityState.Modified;
                                }
                                db.Entry(spamLetter).State = EntityState.Deleted;
                            }
                        }
                        listLetters.AddRange(listSpamLetters);
                    }
                    db.SaveChanges();
                }
            }
            return Content( CustomHelperMetods.MyGrid(listLetters, namberOfPeage.ToString()));
        }


        [Authorize]
        public ActionResult DeleteLetter(int? letterId, int? numberOfType)
        {
            var userId = WebSecurity.CurrentUserId;
            using(var db = new DataBaseContext())
            {
                if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                {
                    var incLetter=db.IncomingLetters.FirstOrDefault(x => x.LetterId == letterId || x.OrderUserId == userId);
                    if (incLetter != null)
                    {
                        var carrentLetter = db.Letters.First(x => x.Id == letterId);
                        carrentLetter.NumberOfOwners = carrentLetter.NumberOfOwners-1;
                        if (carrentLetter.NumberOfOwners == 0)
                        {
                            db.Entry(carrentLetter).State = EntityState.Deleted;
                        }
                        else
                        {
                            db.Entry(carrentLetter).State = EntityState.Modified;
                        }
                        db.Entry(incLetter).State = EntityState.Deleted;
                    }
                }
                if (numberOfType == (int)NumberOfTypes.SendLetters)
                {
                    var sendLetter = db.SendLetters.FirstOrDefault(x => x.LetterId == letterId || x.OrderUserId == userId);
                    if (sendLetter != null)
                    {
                        var carrentLetter = db.Letters.First(x => x.Id == letterId);
                        carrentLetter.NumberOfOwners = carrentLetter.NumberOfOwners - 1;
                        if (carrentLetter.NumberOfOwners == 0)
                        {
                            db.Entry(carrentLetter).State = EntityState.Deleted;
                        }
                        else
                        {
                            db.Entry(carrentLetter).State = EntityState.Modified;
                        }
                        db.Entry(sendLetter).State = EntityState.Deleted;
                    }
                }
                if (numberOfType == (int)NumberOfTypes.SpamLetters)
                {
                    var spamLetter = db.SendLetters.FirstOrDefault(x => x.LetterId == letterId || x.OrderUserId == userId);
                    if (spamLetter != null)
                    {
                        var carrentLetter = db.Letters.First(x => x.Id == letterId);
                        carrentLetter.NumberOfOwners = carrentLetter.NumberOfOwners - 1;
                        if (carrentLetter.NumberOfOwners == 0)
                        {
                            db.Entry(carrentLetter).State = EntityState.Deleted;
                        }
                        else
                        {
                            db.Entry(carrentLetter).State = EntityState.Modified;
                        }
                        db.Entry(spamLetter).State = EntityState.Deleted;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("ShowTypesLetters", "Message", new { numberOfType });
            //return new EmptyResult();
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
            using (var db= new DataBaseContext())
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
                letter.Date = DateTime.Now;
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
        public ActionResult ShowTypesLetters(int? numberOfType, int? namberOfPeage)
        {
            var currentUserId = WebSecurity.CurrentUserId;
            var listLetters = new List<ITypesOfLetter>();
            if (numberOfType != null)
            {
                using (var db = new DataBaseContext())
                {
                    if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                    {
                        var list = db.IncomingLetters.Where(x => x.OrderUserId == currentUserId).OrderBy(x => x.Date).ToList();
                        foreach (var item in list)
                        {
                            listLetters.Add(item);
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
                        ViewBag.NamberOfPeage = namberOfPeage == null? 1 : namberOfPeage;
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

                letter.FilePuth = filePuth;
                letter.FileType = upload.ContentType;
                letter.FileName = upload.FileName;
            }
            letter.FromWhom = user.UserMail;
            letter.NumberOfOwners = allRecipients.Count + 1; //"+1" This is the user who sent the lette

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
        public ITypesOfLetter ReturnTypeOfLetters(int? numberOfType)
        {
            if (numberOfType != null)
            {
                if (numberOfType == (int)NumberOfTypes.IncomingLetters)
                {
                    return new IncomingLetter();
                }
                else if (numberOfType == (int)NumberOfTypes.SendLetters)
                {
                    return new SendLetter();
                }
                else if (numberOfType == (int)NumberOfTypes.SpamLetters)
                {
                    return new SpamLetter();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

           
        }
    }
}