using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mobileOperator = new MobileOperator();
            mobileOperator.AddAAccount(new MobileAccount(111, "zzz", "aaa", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(222, "www", "sss", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(333, "eee", "ddd", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(444, "rrr", "fff", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(555, "ttt", "ggg", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(666, "yyy", "hhh", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(777, "uuu", "jjj", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(888, "qwe", "asd", "saukr.net"));
            mobileOperator.AddAAccount(new MobileAccount(999, "wer", "sdf", "saukr.net"));

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

            SerializeClass.XMLSerialize<MobileOperator> (mobileOperator, "mobileOperator.xml");
            SerializeClass.JsonSerialize<MobileOperator>(mobileOperator, "mobileOperator.json");

            var mobileOperatorClone1 = DeSerializeClass.ReadObjectXML<MobileOperator>("mobileOperator.xml");
            var mobileOperatorClone2 = DeSerializeClass.ReadObjectJson<MobileOperator>("mobileOperator.json");

            Console.ReadKey();


        }

    }
}
