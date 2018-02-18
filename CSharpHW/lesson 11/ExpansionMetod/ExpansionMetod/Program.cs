using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Printer;



namespace ExpansionMetod
{

    class Program
    {
        static void Main(string[] args)
        {
            Printer.Printer printer = new Printer.Printer();
            ColourPrinter colourPrinter = new ColourPrinter();
            PhotoPrinter photoPrinter = new PhotoPrinter();
            string[] message = { "message1", "message2", "message3" };
            ConsoleColor[] color = { ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Magenta };
            string[] photo = { "photo1", "photo2", "photo3" };

            printer.ExpansionToPrinter(message, 0);
            colourPrinter.ExpansionToColourPrinte(message, color, 1);
            colourPrinter.ExpansionToPrinter(message, 1);
            photoPrinter.ExpansionToPhotoPrinter(message, photo, 2);
            photoPrinter.ExpansionToPrinter(message, 2);

            Console.ReadKey();

        }
    }
}
