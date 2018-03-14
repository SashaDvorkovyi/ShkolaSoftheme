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
            Console.WriteLine("This telephone have number" + this.Number);
            var account = sender as MobileAccount;
            if (account != null)
            {
                foreach(var item in addressBook)
                {
                    if (account.Number == item.Key)
                    {
                        Console.WriteLine(item.Value);
                    }
                }
            }
            Console.WriteLine(account.Number);
            if (e.Message != null)
            {
                Console.WriteLine(e.Message);
            }
            else
            {
                Console.WriteLine("Coll");
            }
            
        }
    }
}
