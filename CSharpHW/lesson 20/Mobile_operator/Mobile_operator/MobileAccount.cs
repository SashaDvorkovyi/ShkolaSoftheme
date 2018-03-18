using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Mobile_operator
{
    class MobileAccount
    {
        public event EventHandler<EEventArgs> MessageEvent;
        public event EventHandler<EEventArgs> CallEvent;

        public MobileAccount(int number, string firstNume, string secondNume, string email)
        {
            Number = number;
            FirstNume = firstNume;
            SecondNume = secondNume;
            Email = email;
            addressBook = new Dictionary<int, string>();
        }

        public Dictionary<int, string> addressBook { get; set; }
        [Required]
        [Range(3,10)]
        public int Number { get; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string FirstNume { get; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string SecondNume { get; }
        [EmailAddress]
        [StringLength(50, MinimumLength = 3)]
        public string Email { get; }

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
