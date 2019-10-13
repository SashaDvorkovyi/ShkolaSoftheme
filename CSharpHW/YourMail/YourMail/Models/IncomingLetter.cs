using System;
using System.ComponentModel.DataAnnotations;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class IncomingLetter : ITypesOfLetter
    {
        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        public DateTime? Data { get; set; }

        public string Subject { get; set; }

        public string FromWhom { get; set; }

        public string ToWhoms { get; set; }

        public virtual UserProfile OrderUser { get; set; }

        public virtual LetterForDB LetterForDB { get; set; }
    }
}