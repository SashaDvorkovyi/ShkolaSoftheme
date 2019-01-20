using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourMail.Models
{
    public class Letter
    {
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
    }
}