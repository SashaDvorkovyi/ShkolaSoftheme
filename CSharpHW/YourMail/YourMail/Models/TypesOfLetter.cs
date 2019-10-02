using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourMail.Models
{
    public class TypesOfLetter
    {
        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        public string Subject { get; set; }

        public string ToOrFromWhomMail { get; set; }

        public DateTime? Date { get; set; }

        public int? OrderUserId { get; set; }

        public int? LetterId { get; set; }

        public virtual UserProfile OrderUser { get; set; }

        public virtual Letter Letter { get; set; }
    }
}