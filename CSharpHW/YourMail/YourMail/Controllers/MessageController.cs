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

                var ok1 = SaveNewLetterInDB(letter, user, db);

                var ok2 = SaveITypeOfLetterInDB<SendLetter>(letter, db.SendLetters, user, letter.ToWhom, db);



                var listRecipientUserProfile = new List<UserProfile>();
                var listRecipientPerson = new List<string>();
                var mail = default(string);
                foreach (char x in letter.ToWhom)
                {
                    
                    if (x != ' ')
                    {
                        if (mail[mail.Length - 1] == ',' || mail[mail.Length - 1] == ';')
                        {
                            mail.Substring(0, mail.Length - 1);
                        }
                        else
                        {
                            listRecipientPerson.Add(mail);
                            listRecipientUserProfile.Add(new UserProfile(mail));
                            mail = default(string);
                        }
                    }
                    else
                    {
                        mail += x;
                    }
                }
                db.UserProfiles.Join(listRecipientPerson, x => x.UserMail, y => y, (x, y) => x);

                var res = Splitting.Join(Customer,
                 s => s.CustomerId,
                 c => c.Id,
                 (s, c) => new { s, c })
           .Where(sc => sc.c.Id == userId && sc.c.CompanyId == companId)
           .Select(sc => sc.s);
                db.UserProfiles.Intersect(listRecipientUserProfile, new UserProfile());
                //SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate
                //FROM Orders
                //INNER JOIN Customers ON Orders.CustomerID = Customers.CustomerID;
                var b= from x in db.UserProfiles 
                       from y in listRecipientPerson

                var a = SELECT Orders.OrderID, Customers.CustomerName, Orders.OrderDate
 FROM Orders
 INNER JOIN Customers
ON Orders.CustomerID = Customers.CustomerID;
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

        public bool SaveITypeOfLetterInDB<T>(Letter letter, DbSet<T> tebl, UserProfile userOrder, string userToOrFromWhomMail, DataBaseContext db) where T : class, ITypesOfLetter, new ()
        {
            try
            {
                var TLetter = new T();

                if (userOrder.CountAllSpamLetters < UserProfile.MaxIncomingLetters)
                {
                    TLetter = tebl.FirstOrDefault(x => x.IsExist == false);
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
                TLetter.OrderMail= userOrder.UserMail;
                db.Entry(TLetter).State = EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}