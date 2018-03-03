using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericTipe
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var obj = new ListGeneric<int>(array);
            obj.AddElement(11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21);
            Console.WriteLine(obj.Lenght());
            obj.DeleteElement(10);
            Console.WriteLine(obj.Lenght());
            obj.DeleteElement(10);
            Console.WriteLine(obj.Lenght());
            Console.WriteLine(obj.Equals(11));
            int[] array2 = obj.ToArray();
            Console.WriteLine();
            foreach(var item in array2)
            {
                Console.Write(item + ", ");
            }
            Console.ReadKey();
        }
    }
}
