using System;
using System.ComponentModel.DataAnnotations;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class SendLetter : TypesOfLetter, ITypesOfLetter
    {
    }
}