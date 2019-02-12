using System;
using System.ComponentModel.DataAnnotations;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class SendLetter : ITypesOfLetter
    {
        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; }

        public string ToOrFromWhomMail { get; set; }

        public DateTime? Date { get; set; }

        public int? OrderId { get; set; }

        public virtual UserProfile OrderUser { get; set; }

        public int? LetterId { get; set; }

        public virtual Letter Letter { get; set; }
    }
}