using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            var user = new MobileAccount(6334, "ssss", "aaa", "s@ukr.net");
            Console.WriteLine(user.FirstNume);
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            Console.WriteLine(user.FirstNume);
            var mobileOperator = new MobileOperator();
            mobileOperator.AddAAccount(new MobileAccount(111, "", "aaa", ""));
            mobileOperator.AddAAccount(new MobileAccount(222, "www", "sss", "xxx"));
            mobileOperator.AddAAccount(new MobileAccount(333, "eee", "ddd", "ccc"));
            mobileOperator.AddAAccount(new MobileAccount(444, "rrr", "fff", "vvv"));
            mobileOperator.AddAAccount(new MobileAccount(555, "ttt", "ggg", "bbb"));
            mobileOperator.AddAAccount(new MobileAccount(666, "yyy", "hhh", "nnn"));
            mobileOperator.AddAAccount(new MobileAccount(777, "uuu", "jjj", "mmm"));
            mobileOperator.AddAAccount(new MobileAccount(888, "qwe", "asd", "zxc"));
            mobileOperator.AddAAccount(new MobileAccount(999, "wer", "sdf", "xcv"));


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
