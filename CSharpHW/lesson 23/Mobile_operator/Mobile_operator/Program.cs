using System;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            //var mobileOperator = DeSerializeClass.ReadObjectJson<MobileOperator>("mobileOperator.json");
            //var mobileOperator2 = DeSerializeClass.ReadObjectXML<MobileOperator>("mobileOperator.xml");

            var mobileOperator = new MobileOperator();
            mobileOperator.AddAAccount(new MobileAccount(2222, "dsd", "dssdd", "asy@ukr.net"));

            //if (mobileOperator != null)
            //{
            //    mobileOperator.Top_5_Outgoing();
            //    Console.WriteLine();
            //    mobileOperator.Top_5_Ingoing();
            //}
            Console.ReadKey();
        }
    }
}
