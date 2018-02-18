using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printer
{
    public class PhotoPrinter :Printer
    {
        public override void Print(string s)
        {
            base.Print(s);
        }
        public void Print(string s, string fotoName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PhotoPrinter: " + s+". Photo: "+ fotoName);
            Console.ResetColor();
        }
    }
}
