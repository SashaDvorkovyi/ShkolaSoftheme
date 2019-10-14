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
                var letter = db.LettersForDB.FirstOrDefault(x => x.Id == letterId);
                if (letter != null)
                {
                    if ((letter.SendLetters.Any(x => x.OrderUser.Id == userId)) 
                        || (letter.IncomingLetters.Any(x => x.OrderUser.Id == userId)) 
                        || (letter.SendLetters.Any(x => x.OrderUser.Id == userId)))
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
                        ViewBag.NumberOfType = numberOfType;
                        return View(new Letter(typeOfLetter.LetterForDB, typeOfLetter));
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public ActionResult DeleteAllSelected( int?[] arrayIdOfLetters, int? numberOfType, int? namberOfPeage)
        {
            var listLetters = new List<ITypesOfLetter>();

            if (arrayIdOfLetters != null)
            {
                var userId = WebSecurity.CurrentUserId;
                using (var db = new DataBaseContext())
                {
                    if (numberOfType != null && numberOfType < 3)
                    {
                        var listTypeOfLetter = (db.listTypesOfLetter[(int)numberOfType]
                                               .Join(arrayIdOfLetters, x => x.Id, y => y, (x, y) => x)
                                               .Where(x => x.OrderUser.Id == userId))
                                               .Include(x => x.LetterForDB);

                        if (listTypeOfLetter != null)
                        {
                            foreach (var typeOfLetter in listTypeOfLetter)
                            {
                                typeOfLetter.LetterForDB.NumberOfOwners--;

                                if (typeOfLetter.LetterForDB.NumberOfOwners == 0)
                                {
                                    db.Entry(typeOfLetter.LetterForDB).State = EntityState.Deleted;
                                }
                                else
                                {
                                    db.Entry(typeOfLetter.LetterForDB).State = EntityState.Modified;
                                }
                                db.Entry(typeOfLetter).State = EntityState.Deleted;
                            }
                        }
                    }
                    db.SaveChanges();
                    listLetters = db.listTypesOfLetter[(int)numberOfType].Where(x => x.OrderUser.Id == userId).OrderByDescending(x=>x.Data).ToList();
                }
            }
            return Content(CustomHelperMetods.MyGrid(listLetters, namberOfPeage.ToString()));
        }

        [Authorize]
        public ActionResult ChangePage(int? numberOfType, int? namberOfPeage)
        {
            var listLetters = new List<ITypesOfLetter>();

            var userId = WebSecurity.CurrentUserId;

            if (numberOfType != null && numberOfType < 3)
            {
                using (var db = new DataBaseContext())
                {
                    listLetters = db.listTypesOfLetter[(int)numberOfType].Where(x => x.OrderUser.Id == userId).OrderBy(x=>x.Data).ToList();
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
                    var typeLetter = db.listTypesOfLetter[(int)numberOfType].FirstOrDefault(x => x.LetterForDB.Id == letterId || x.OrderUser.Id == userId);
                    if (typeLetter != null)
                    {
                        if (typeLetter.LetterForDB.NumberOfOwners == 0)
                        {
                            db.Entry(typeLetter.LetterForDB).State = EntityState.Deleted;
                        }
                        else
                        {
                            db.Entry(typeLetter.LetterForDB).State = EntityState.Modified;
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
            newLetter.ToWhoms = fromWhom;
            return View("New_letter", newLetter);
        }
        [Authorize]
        public ActionResult ForwardLetter(int? letterId, int? numberOfType)
        {
            if (numberOfType != null && letterId != null && numberOfType < 3 && numberOfType >= 0)
            {
                var userName = User.Identity.Name;
                ViewBag.user = userName;
                using (var db = new DataBaseContext())
                {
                    var typeOfLetter = db.listTypesOfLetter[(int)numberOfType].FirstOrDefault(x => x.LetterForDB.Id == letterId && userName == x.OrderUser.UserMail);
                    if (typeOfLetter != null)
                    {
                        typeOfLetter.ToWhoms = "";
                        return View("New_letter", new Letter(typeOfLetter.LetterForDB, typeOfLetter));
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult New_letter(Letter letter, HttpPostedFileBase upload)
        {
            if (letter != null)
            {
                using (var db = new DataBaseContext())
                {
                    var listAllRecidients = GetAllRecipients(letter);
                    var listToWhomUserProfile = db.UserProfiles.Join(listAllRecidients, x => x.UserMail, y => y, (x, y) => x)
                                                                    .Include(x => x.IncomingLetters).Include(x => x.SendLetters)
                                                                    .Include(x => x.SpamLetters).Include(x => x.SpamMeils).ToList();

                    var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);

                    letter.FromWhom = user.UserMail;
                    letter.Data = DateTime.Now;

                    var letterForDB = CreateNewLetterForDB(letter, user, listToWhomUserProfile.Count, upload);

                    db.LettersForDB.Add(letterForDB);

                    DeleteTypeFoLetterIfCountMoreThenMAX(UserProfile.MaxSendLetters, (int)NumberOfTypes.SendLetters, db, user);
                    db.SendLetters.Add(CreateTypeOfLetter<SendLetter>(letter, user, letterForDB));

                    foreach(var toWhom in listToWhomUserProfile)
                    {
                        if (toWhom.SpamMeils.Count !=0)
                        {
                            if (toWhom.SpamMeils.Any(x => x.ToWhomMail == user.UserMail))
                            {
                                DeleteTypeFoLetterIfCountMoreThenMAX(UserProfile.MaxSpamLetters, (int)NumberOfTypes.SpamLetters, db, user);
                                db.SpamLetters.Add(CreateTypeOfLetter<SpamLetter>(letter, toWhom, letterForDB));
                            }
                        }
                        else
                        {
                            DeleteTypeFoLetterIfCountMoreThenMAX(UserProfile.MaxIncomingLetters, (int)NumberOfTypes.IncomingLetters, db, user);
                            db.IncomingLetters.Add(CreateTypeOfLetter<IncomingLetter>(letter, toWhom, letterForDB));
                        }
                    }

                    db.SaveChanges(); 
                }
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
                db.UserProfiles.Min(x=>x.Id);
                if (user != null)
                {
                    if (user.SpamMeils.Count==UserProfile.MaxSpamMail)
                    {
                        db.Entry(user.SpamMeils.Where(x=>x.Id == user.SpamMeils.Min(y => y.Id))).State = EntityState.Deleted;
                    }
                    db.Entry(spamMeil).State = EntityState.Added;

                    db.SaveChanges();
                }
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
                    if (numberOfType != null && numberOfType < 3 && numberOfType >= 0)
                    {
                        listLetters = db.listTypesOfLetter[(int)numberOfType].Where(x => x.OrderUser.Id == currentUserId).OrderByDescending(x => x.Data).ToList();

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

        public void DeleteTypeFoLetterIfCountMoreThenMAX(int MAXCoun, int numberOfType, DataBaseContext db, UserProfile user)
        {
            if(db.listTypesOfLetter[numberOfType].Count(x => x.OrderUser.Id == user.Id) == MAXCoun)
            {
                var someTypeOfLetter=db.listTypesOfLetter[numberOfType].First(x => x.OrderUser.Id == user.Id && x.Data == db.listTypesOfLetter[numberOfType].Min(y => y.Data));
                db.Entry(someTypeOfLetter).State = EntityState.Deleted;
            }
        }

        public T CreateTypeOfLetter<T>(Letter letter, UserProfile user, LetterForDB letterForDB) where T : ITypesOfLetter, new()
        {
            var typeOfLetter = new T();
            typeOfLetter.LetterForDB = letterForDB;
            typeOfLetter.OrderUser = user;
            typeOfLetter.Subject = letter.Subject;
            typeOfLetter.Data = letter.Data;
            typeOfLetter.FromWhom = letter.FromWhom;
            typeOfLetter.ToWhoms = letter.ToWhoms;
            return typeOfLetter;
        }

        public LetterForDB CreateNewLetterForDB(Letter letter, UserProfile user, int allRecipientsCount, HttpPostedFileBase upload)
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
            letter.NumberOfOwners = allRecipientsCount + 1; //"+1" This is the user who sent the lette

            return (LetterForDB)letter;
        }

        public List<string> GetAllRecipients(Letter letter)
        {
            var listRecipientPerson = new List<string>();
            var mail = default(string);
            foreach (char x in letter.ToWhoms)
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