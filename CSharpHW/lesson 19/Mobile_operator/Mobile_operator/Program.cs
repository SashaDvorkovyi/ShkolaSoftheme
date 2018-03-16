using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mobileOperator = new MobileOperator();
            mobileOperator.AddAAccount(111);
            mobileOperator.AddAAccount(222);
            mobileOperator.AddAAccount(333);
            mobileOperator.AddAAccount(444);
            mobileOperator.AddAAccount(555);
            mobileOperator.AddAAccount(666);
            mobileOperator.AddAAccount(777);
            mobileOperator.AddAAccount(888);
            mobileOperator.AddAAccount(999);

            mobileOperator.TakeAccount(222).addressBook.Add(111, "Artem");
            mobileOperator.TakeAccount(111).Call(222);
            mobileOperator.TakeAccount(111).SendMessage(222, "hello");

            mobileOperator.Top_5_Outgoing();
            Console.ReadKey();

        }
    }
}
