using System;
using System.ComponentModel.DataAnnotations;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class IncomingLetter : TypesOfLetter, ITypesOfLetter
    {
    }
}