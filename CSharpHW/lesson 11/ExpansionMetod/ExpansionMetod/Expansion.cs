using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Printer;

namespace ExpansionMetod
{
    public static class Expansion
    {
        public static void ExpansionToPrinter(this Printer.Printer a, string[] message, int i)
        {
            Console.WriteLine("Printer: " + message[i]);
        }

        public static void ExpansionToColourPrinte(this ColourPrinter a, string[] message, ConsoleColor[] color, int i)
        {
            Console.ForegroundColor = color[i];
            Console.WriteLine("ColourPrinter: " + message[i]);
            Console.ResetColor();
        }

        public static void ExpansionToPhotoPrinter(this PhotoPrinter a, string[] message, string[] photoName, int i)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PhotoPrinter: " + message[i] + ". Photo: " + photoName[i]);
            Console.ResetColor();
        }
    }
}
