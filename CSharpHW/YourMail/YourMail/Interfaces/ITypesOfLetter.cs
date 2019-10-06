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

        bool IsRead { get; set; }

        DataType Data { get; set; }

        string Subject { get; set; }

        string FromWhom { get; set; }

        string ToWhoms { get; set; }

        UserProfile OrderUser { get; set; }

        Letter Letter { get; set; }
    }
}