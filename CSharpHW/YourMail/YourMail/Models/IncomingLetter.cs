using System;
using YourMail.Interfaces;

namespace YourMail.Models
{
    public class IncomingLetter : ITypesOfLetter
    {
        public IncomingLetter(int orderId)
        {
            this.OrderId = orderId;
        }

        public int Id { get; set; }

        public bool IsExist { get; set; }

        public bool? IsRead { get; set; }

        public int? OrderId { get; set; }

        public string Subject { get; set; }

        public string ToOrFromWhomMail { get; set; }

        public DateTime? Date { get; set; }
    }
}