using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer
{
    public class ColourPrinter : Printer
    {
        public override void Print(string s)
        {
            base.Print(s);
        }
        public void Print(string s, ConsoleColor a)
        {
            Console.ForegroundColor = a;
            Console.WriteLine("ColourPrinter: " + s);
            Console.ResetColor();
        }
    }
}
