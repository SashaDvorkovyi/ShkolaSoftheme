using System;
using System.ComponentModel.DataAnnotations;
using YourMail.Models;

namespace YourMail.Interfaces
{
    public interface ITypesOfLetter
    {
        [Key]
        int Id { get; set; }

        bool IsRead { get; set; }

        DateTime? Data { get; set; }

        string Subject { get; set; }

        string FromWhom { get; set; }

        string ToWhoms { get; set; }

        UserProfile OrderUser { get; set; }

        LetterForDB LetterForDB { get; set; }
    }
}