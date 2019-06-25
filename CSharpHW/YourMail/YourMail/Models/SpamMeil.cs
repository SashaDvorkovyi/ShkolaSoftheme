using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourMail.Models
{
    public class SpamMeil
    {
        [Key]
        public int Id { get; set; }

        public int? OrderUserId { get; set; }

        [EmailAddress]
        public string ToWhomMail { get; set; }

        public virtual UserProfile OrderUser { get; set; }
    }
}