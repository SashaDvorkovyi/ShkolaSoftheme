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
            SpamMeils = new List<SpamMeil>();
        }

        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        [StringLength(50, ErrorMessage = "The long must be at least {0} characters.")]
        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public string FromWhom { get; set; }

        public string ToWhom { get; set; }

        string NameOfFile { get; set; }

        string AdresOfFile { get; set; }

        public virtual ICollection<IncomingLetter> IncomingLetters { get; set; }
        public virtual ICollection<SendLetter> SendLetters { get; set; }
        public virtual ICollection<SpamLetter> SpamLetters { get; set; }
        public virtual ICollection<SpamMeil> SpamMeils { get; set; }
    }
}