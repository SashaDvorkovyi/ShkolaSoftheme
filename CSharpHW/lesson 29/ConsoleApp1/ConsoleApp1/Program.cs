using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Multiply(3, 4));
        }

        private static double Multiply(double x, double y)
        {
            var z = x * y;
            return z;
        }

        private static double GetPi()
        {
            return Math.PI;
        }
    }
}
