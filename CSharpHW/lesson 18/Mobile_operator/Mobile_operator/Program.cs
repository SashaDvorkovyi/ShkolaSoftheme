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
            mobileOperator.listAccount.Add(new MobileAccount(1234567));
            mobileOperator.listAccount.Add(new MobileAccount(1111111));
            mobileOperator.listAccount.Add(new MobileAccount(2222222));
            mobileOperator.listAccount.Add(new MobileAccount(3333333));
            mobileOperator.listAccount.Add(new MobileAccount(4444444));
            mobileOperator.listAccount.Add(new MobileAccount(5555555));
            mobileOperator.listAccount.Add(new MobileAccount(6666666));
            foreach(var item in mobileOperator.listAccount)
            {
                item.MessageEvent += mobileOperator.AcceptAndSend;
                item.CallEvent+= mobileOperator.AcceptAndSend;
            }
            mobileOperator.listAccount[1].Call(2222222);
            Console.ReadKey();

        }
    }
}
