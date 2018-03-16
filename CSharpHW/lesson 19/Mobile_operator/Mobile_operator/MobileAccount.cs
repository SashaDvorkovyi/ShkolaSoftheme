using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class MobileAccount
    {
        public event EventHandler<EEventArgs> MessageEvent;
        public event EventHandler<EEventArgs> CallEvent;

        public Dictionary<int, string> addressBook { get; set; }

        public int Number { get; private set; }

        public MobileAccount(int number)
        {
            Number = number;
            addressBook = new Dictionary<int, string>();
        }

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
                var acc = (from i in addressBook
                           where i.Key == account.Number
                           select i).First();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(acc.Value);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(account.Number);
            Console.ResetColor();
        }
    }
}
