using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YourMail.Interfaces
{
    public interface ITypesOfLetter
    {
        int Id {get; set;}

        bool IsExist { get; set; }

        bool? IsRead { get; set; }

        int? OrderId { get; set; }

        string Subject { get; set; }

        string ToOrFromWhomMail { get; set; }

        DateTime? Date { get; set; }
    }
}