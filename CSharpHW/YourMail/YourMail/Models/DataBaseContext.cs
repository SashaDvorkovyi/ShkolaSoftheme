using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace YourMail.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<IncomingLetter> IncomingLetters { get; set; }
        public DbSet<SendLetter> SendLetters { get; set; }
        public DbSet<SpamLetter> SpamLetters { get; set; }
        public DbSet<SpamMeil> SpamMeils { get; set; }
    }
}