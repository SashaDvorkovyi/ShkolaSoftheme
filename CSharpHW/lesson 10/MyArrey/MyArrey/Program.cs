using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrey
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            for(var i=0; i<50; i++)
            {
                list.Add(i);
            }

            list.Show();

            Console.WriteLine();
            Console.WriteLine(list.Contains(40));
            Console.WriteLine(list.Lenght());
            Console.WriteLine(list.GetByIndex(20));

            Console.ReadKey();

        }
    }
}
