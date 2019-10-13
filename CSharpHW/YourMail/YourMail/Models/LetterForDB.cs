using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YourMail.Models
{
    public class LetterForDB
    {
        public LetterForDB()
        {
            IncomingLetters = new List<IncomingLetter>();
            SendLetters = new List<SendLetter>();
            SpamLetters = new List<SpamLetter>();
        }

        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string FileName { get; set; }

        public string FilePuth { get; set; }

        public string FileType { get; set; }

        public string AdresOfFile { get; set; }

        public int NumberOfOwners { get; set; }

        public virtual ICollection<IncomingLetter> IncomingLetters { get; set; }
        public virtual ICollection<SendLetter> SendLetters { get; set; }
        public virtual ICollection<SpamLetter> SpamLetters { get; set; }
    }
}