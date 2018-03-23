using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mobileOperator = new MobileOperator();
            mobileOperator.AddAAccount(new MobileAccount(111, "zzz", "aaa", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(222, "www", "sss", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(333, "eee", "ddd", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(444, "rrr", "fff", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(555, "ttt", "ggg", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(666, "yyy", "hhh", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(777, "uuu", "jjj", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(888, "qwe", "asd", "s@ukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(999, "wer", "sdf", "s@ukr.net"));

            try
            {
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
            }
            catch
            {
                Console.WriteLine("No number in the database!!");
            }
            
            mobileOperator.Top_5_Outgoing();
            Console.WriteLine();
            mobileOperator.Top_5_Ingoing();


            //mobileOperator.XMLSerializ();

            Console.ReadKey();


        }

    }
}
