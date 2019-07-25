using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourMail.Models
{
    public class Letter
    {
        public Letter()
        {
            Date = DateTime.Now;
            IncomingLetters = new List<IncomingLetter>();
            SendLetters = new List<SendLetter>();
            SpamLetters = new List<SpamLetter>();
        }

        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        [StringLength(50, ErrorMessage = "The long must be at least {0} characters.")]
        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public string FromWhom { get; set; }

        public string ToWhom { get; set; }

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