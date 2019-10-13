using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class Letter : LetterForDB
    {
        public Letter()
        {
            IncomingLetters = new List<IncomingLetter>();
            SendLetters = new List<SendLetter>();
            SpamLetters = new List<SpamLetter>();
        }

        public Letter(LetterForDB letterForDB, ITypesOfLetter typeOfLetter)
        {
            Id = letterForDB.Id;
            Content = letterForDB.Content;
            FileName = letterForDB.FileName;
            FilePuth = letterForDB.FilePuth;
            FileType = letterForDB.FileType;
            AdresOfFile = letterForDB.AdresOfFile;
            NumberOfOwners = letterForDB.NumberOfOwners;
            IncomingLetters = letterForDB.IncomingLetters;
            SendLetters = letterForDB.SendLetters;
            SpamLetters = letterForDB.SpamLetters;

            Subject = typeOfLetter.Subject;
            Data = typeOfLetter.Data;
            FromWhom = typeOfLetter.FromWhom;
            ToWhoms = typeOfLetter.ToWhoms;

        }

        [StringLength(50, ErrorMessage = "The long must be at least {0} characters.")]
        public string Subject { get; set; }

        public DateTime? Data { get; set; }

        public string FromWhom { get; set; }

        [Required]
        public string ToWhoms { get; set; }
    }
}