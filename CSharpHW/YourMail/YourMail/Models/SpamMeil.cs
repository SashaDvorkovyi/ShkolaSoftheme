using System.ComponentModel.DataAnnotations;

namespace YourMail.Models
{
    public class SpamMeil
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        public string ToWhomMail { get; set; }

        public virtual UserProfile OrderUser { get; set; }
    }
}