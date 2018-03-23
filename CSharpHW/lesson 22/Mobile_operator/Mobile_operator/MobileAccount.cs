using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Mobile_operator
{
    [DataContract]
    public class MobileAccount 
    {
        public event EventHandler<EEventArgs> MessageEvent;
        public event EventHandler<EEventArgs> CallEvent;

        [Required]
        [Range(99, 999999999)]
        [DataMember]
        public readonly int Number;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataMember]
        public readonly string FirstNume;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataMember]
        public readonly string SecondNume;

        [EmailAddress]
        [StringLength(50, MinimumLength = 3)]
        [DataMember]
        public readonly string Email;

        public MobileAccount(int number, string firstNume, string secondNume, string email)
        {
            Number = number;
            FirstNume = firstNume;
            SecondNume = secondNume;
            Email = email;
            addressBook = new Dictionary<int, string>();
        }

        [DataMember]
        public Dictionary<int, string> addressBook { get; set; }

        public string SendMessage(int number,string str)
        {
            if (MessageEvent != null)
            {
                MessageEvent(this, new EEventArgs(number, str));
            }
            return str;
        }

        public void Call(int number)
        {
            if (CallEvent != null)
            {
                CallEvent(this, new EEventArgs(number));
            }
        }

        public void Show(object sender, EEventArgs e)
        {
            Console.Write("This telephone have number: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(this.Number);
            Console.ResetColor();

            var account = sender as MobileAccount;
            if (e.Message != null)
            {
                Console.Write("Message text: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                Console.Write("Message from: ");
            }
            else
            {
                Console.Write("Coll from: ");
            }

            if (account != null)
            {
                var acc = addressBook.FirstOrDefault(x => x.Key == account.Number);
                Console.ForegroundColor = ConsoleColor.Green;
                if (acc.Value != null)
                {
                    Console.WriteLine(acc.Value);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(account.Number);
            Console.ResetColor();
        }
    }
}
