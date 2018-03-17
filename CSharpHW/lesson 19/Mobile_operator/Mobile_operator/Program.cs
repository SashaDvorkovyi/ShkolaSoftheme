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
            mobileOperator.TakeAccount(333).Call(222);
            mobileOperator.TakeAccount(111).SendMessage(555, "hello");
            mobileOperator.TakeAccount(222).Call(222);
            mobileOperator.TakeAccount(111).SendMessage(222, "hello");
            mobileOperator.TakeAccount(111).Call(666);
            mobileOperator.TakeAccount(444).SendMessage(444, "hello");
            mobileOperator.TakeAccount(111).Call(222);
            mobileOperator.TakeAccount(111).SendMessage(222, "hello");
            mobileOperator.TakeAccount(111).Call(222);
            mobileOperator.TakeAccount(111).SendMessage(888, "hello");
            mobileOperator.TakeAccount(777).Call(222);
            mobileOperator.TakeAccount(555).SendMessage(666, "hello");
            mobileOperator.TakeAccount(888).Call(444);
            mobileOperator.TakeAccount(111).SendMessage(333, "hello");
            mobileOperator.TakeAccount(444).Call(999);
            mobileOperator.TakeAccount(111).SendMessage(777, "hello");
            mobileOperator.TakeAccount(888).Call(555);
            mobileOperator.TakeAccount(666).SendMessage(111, "hello");
            mobileOperator.Top_5_Outgoing();
            Console.WriteLine();
            mobileOperator.Top_5_Ingoing();
            Console.ReadKey();

        }
    }
}
