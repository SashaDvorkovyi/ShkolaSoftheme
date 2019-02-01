using System;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class SendLetter : ITypesOfLetter
    {
        public SendLetter() { }

        public SendLetter(string orderMail)
        {
            this.OrderMail = orderMail;
        }

        public int Id { get; set; }

        public bool IsExist { get; set; }

        public bool IsRead { get; set; }

        public string OrderMail { get; set; }

        public string Subject { get; set; }

        public string ToOrFromWhomMail { get; set; }

        public DateTime? Date { get; set; }
    }
}