using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class DataBaseContext : DbContext
    {
        public List<IQueryable<ITypesOfLetter>> listTypesOfLetter;

        public DataBaseContext()
            : base("DefaultConnection")
        {
            listTypesOfLetter = new List<IQueryable<ITypesOfLetter>>();
            listTypesOfLetter.Add(IncomingLetters);
            listTypesOfLetter.Add(SendLetters);
            listTypesOfLetter.Add(SpamLetters);
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<LetterForDB> LettersForDB { get; set; }
        public DbSet<IncomingLetter> IncomingLetters { get; set; }
        public DbSet<SendLetter> SendLetters { get; set; }
        public DbSet<SpamLetter> SpamLetters { get; set; }
        public DbSet<SpamMeil> SpamMeils { get; set; }
    }
}