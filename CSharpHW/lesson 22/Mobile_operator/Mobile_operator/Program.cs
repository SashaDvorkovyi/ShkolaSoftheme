using System;

namespace Mobile_operator
{
    class Program
    {
        static void Main(string[] args)
        {
            var mobileOperator = default(MobileOperator);
            try
            {
                mobileOperator = DeSerializeClass.ReadObjectJson<MobileOperator>("mobileOperator.json");
            }
            catch(Exception)
            {
                Console.WriteLine("There is no deserialization file");
            }

            if (mobileOperator != null)
            {
                mobileOperator.Top_5_Outgoing();
                Console.WriteLine();
                mobileOperator.Top_5_Ingoing();
            }
            Console.ReadKey();
        }
    }
}
