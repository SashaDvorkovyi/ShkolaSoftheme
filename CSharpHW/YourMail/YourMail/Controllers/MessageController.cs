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
            using (var db = new DataBaseContext())
            {
                var user = db.UserProfiles.FirstOrDefault(x => x.UserMail == User.Identity.Name);

                SaveNewLetterInDB(letter, user, db);

                db.Entry(SaveITypeOfLetterInDB<SendLetter>(letter, db.SendLetters, user, letter.ToWhom)).State = EntityState.Added;


                db.Entry(ChengeUser<SendLetter>(db.SendLetters, user)).State = EntityState.Modified;


                var allResipient = GetAllRecipient(letter, db);

                foreach(var resipient in allResipient)
                {
                    var send = false;
                    foreach(var spamMail in resipient.SpamMeils)
                    {
                        if (spamMail.ToWhomMail == user.UserMail)
                        {
                            db.Entry(SaveITypeOfLetterInDB<SpamLetter>(letter, db.SpamLetters, resipient, user.UserMail)).State = EntityState.Added;
                            db.Entry(ChengeUser<SpamLetter>(db.SpamLetters, resipient)).State = EntityState.Modified;
                            send = true;
                            break;
                        }
                    }
                    if (send != true)
                    {
                        db.Entry(SaveITypeOfLetterInDB<IncomingLetter>(letter, db.IncomingLetters, resipient, user.UserMail)).State = EntityState.Added;
                        db.Entry(ChengeUser<IncomingLetter>(db.IncomingLetters, resipient)).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public void SaveNewLetterInDB(Letter letter, UserProfile user, DataBaseContext db)
        {
            letter.FromWhom = user.UserMail;
            db.Letters.Add(letter);
        }

        public T SaveITypeOfLetterInDB<T>(Letter letter, DbSet<T> tebl, UserProfile userOrder, string userToOrFromWhomMail) where T : class, ITypesOfLetter, new()
        {
                var tLetter = new T();

                tLetter.Subject = letter.Subject;

                tLetter.ToOrFromWhomMail = userToOrFromWhomMail;

                tLetter.Date = letter.Date;

                tLetter.OrderId = userOrder.Id;

                tLetter.LetterId = letter.Id;

                return tLetter;
        }

        public IQueryable<UserProfile> GetAllRecipient(Letter letter, DataBaseContext db)
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
            return db.UserProfiles.Join(listRecipientPerson, x => x.UserMail, y => y, (x, y) => x).Include(x=>x.SpamMeils);
        }

        public UserProfile ChengeUser<T>(DbSet<T> tebl, UserProfile userOrder) where T : class, ITypesOfLetter, new()
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
            else if (new T() is SendLetter)
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