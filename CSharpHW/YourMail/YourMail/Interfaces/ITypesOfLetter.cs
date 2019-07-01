using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YourMail.Models;

namespace YourMail.Interfaces
{
    public interface ITypesOfLetter
    {
        [Key]
        int Id { get; set; }

        int NumberOfType { get; set; }

        bool Active { get; set; }

        bool IsRead { get; set; }

        string Subject { get; set; }

        string ToOrFromWhomMail { get; set; }

        DateTime? Date { get; set; }

        int? OrderUserId { get; set; }

        int? LetterId { get; set; }

        UserProfile OrderUser { get; set; }

        Letter Letter { get; set; }
    }
}