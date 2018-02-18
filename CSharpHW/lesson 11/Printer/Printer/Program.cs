using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer
{
    class Program
    {
        static void Main(string[] args)
        {
            ColourPrinter ob1 = new ColourPrinter();
            PhotoPrinter ob2 = new PhotoPrinter();
            ob1.Print("Hello");
            ob1.Print("Hello", ConsoleColor.Red);
            ob2.Print("Hello");
            ob2.Print("Hello", "Family");
            Console.ReadKey();
            
        }
    }
}
