﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class TypesOfLetter : ITypesOfLetter
    {
        [Key]
        public int Id { get; set; }

        public bool IsRead { get; set; }

        public DataType Data { get; set; }

        public string Subject { get; set; }

        public string FromWhom { get; set; }

        public string ToWhoms { get; set; }

        public virtual UserProfile OrderUser { get; set; }

        public virtual Letter Letter { get; set; }
    }
}