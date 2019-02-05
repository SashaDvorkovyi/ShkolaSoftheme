using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using YourMail.Models;
using YourMail.Interfaces;


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

                var okList = new List<bool>();

                okList.Add(SaveNewLetterInDB(letter, user, db));

                okList.Add(SaveITypeOfLetterInDB<SendLetter>(letter, db.SendLetters, user, letter.ToWhom, db));

                var allResipient = GetAllRecipient(letter, db);

                var listSpamEmail = db.SpamMeils.Skip(user.Id * UserProfile.MaxSentLetters - UserProfile.MaxSentLetters)
                                    .Take(UserProfile.MaxSentLetters);

                if (allResipient.Count() != 0)
                {
                    foreach (var userProf in allResipient)
                    {
                        var isSave = false;
                        foreach (var spamEmail in listSpamEmail)
                        {
                            if (spamEmail.ToWhomMail == userProf.UserMail)
                            {
                                okList.Add(SaveITypeOfLetterInDB<SpamLetter>(letter, db.SpamLetters, userProf, user.UserMail, db));
                                isSave = true;
                                break;
                            }
                        }
                        if (isSave == false)
                        {
                            okList.Add(SaveITypeOfLetterInDB<IncomingLetter>(letter, db.IncomingLetters, userProf, user.UserMail, db));
                        }
                    }
                }


            }
            return RedirectToAction("Index", "Home");
        }

        public bool SaveNewLetterInDB(Letter letter, UserProfile user, DataBaseContext db)
        {
            try
            {
                letter.FromWhom = user.UserMail;
                db.Letters.Add(letter);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SaveITypeOfLetterInDB<T>(Letter letter, DbSet<T> tebl, UserProfile userOrder, string userToOrFromWhomMail, DataBaseContext db) where T : class, ITypesOfLetter, new()
        {
            try
            {
                var TLetter = new T();

                if (userOrder.CountAllSpamLetters==null || userOrder.CountAllSpamLetters < UserProfile.MaxIncomingLetters)
                {
                    TLetter = tebl.FirstOrDefault(x => x.IsExist == false);
                    TLetter = tebl.Skip(userOrder.Id * UserProfile.MaxSentLetters - UserProfile.MaxSentLetters)
                                  .First(x => x.IsExist == false);
                }
                else
                {
                    TLetter = tebl.Skip(userOrder.Id * UserProfile.MaxSentLetters - UserProfile.MaxSentLetters)
                                  .Take(UserProfile.MaxSentLetters)
                                  .OrderBy(x => x.Date).First();
                }

                TLetter.Date = letter.Date;
                TLetter.ToOrFromWhomMail = userToOrFromWhomMail;
                TLetter.Subject = letter.Subject;
                TLetter.IsExist = true;
                TLetter.OrderMail = userOrder.UserMail;
                db.Entry(TLetter).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IQueryable<UserProfile> GetAllRecipient(Letter letter, DataBaseContext db)
        {
            var listRecipientPerson = new List<string>();
            var mail = default(string);
            foreach (char x in letter.ToWhom)
            {

                if (x != ' ' && mail != null)
                {
                    if (mail[mail.Length - 1] == ',' || mail[mail.Length - 1] == ';')
                    {
                        mail.Substring(0, mail.Length - 1);
                    }
                    else
                    {
                        listRecipientPerson.Add(mail);
                        mail = default(string);
                    }
                }
                else if(x != ' ' && mail == null)
                {

                }
                else
                {
                    mail += x;
                }
            }
            return db.UserProfiles.Join(listRecipientPerson, x => x.UserMail, y => y, (x, y) => x);
        }
    }
}