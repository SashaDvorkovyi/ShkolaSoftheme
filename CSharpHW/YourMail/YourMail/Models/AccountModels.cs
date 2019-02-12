using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YourMail.Filters;

namespace YourMail.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserMail { get; set; }

        public const int MaxIncomingLetters = 200;
        public const int MaxSentLetters = 200;
        public const int MaxSpamLetters = 200;
        public const int MaxSpamMail = 200;

        public UserProfile()
        {
            IncomingLetters = new List<IncomingLetter>();
            SendLetters = new List<SendLetter>();
            SpamLetters = new List<SpamLetter>();
            SpamMeils = new List<SpamMeil>();
        }

        public int? CountAllIncomingLetters { get; set; }
        public int? CountDontReadIncomingLetters { get; set; }
        public int? CountAllSendLetters { get; set; }
        public int? CountDontReadSendLetters { get; set; }
        public int? CountAllSpamLetters { get; set; }
        public int? CountDontReadSpamLetters { get; set; }

        public virtual ICollection<IncomingLetter> IncomingLetters { get; set; }
        public virtual ICollection<SendLetter> SendLetters { get; set; }
        public virtual ICollection<SpamLetter> SpamLetters { get; set; }
        public virtual ICollection<SpamMeil> SpamMeils { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User email")]
        public string UserMail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [MyMailAddress]
        [Display(Name = "User email")]
        public string UserMail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}