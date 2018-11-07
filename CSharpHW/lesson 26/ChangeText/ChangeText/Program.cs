using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace ChangeText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pleas enter the all name of directiry");
            var directiry = new DirectoryInfo(Console.ReadLine());
            //Console.WriteLine("Pleas enter the type of text file. Example - 'txt' ");
            //var tipeOfFile = Console.ReadLine();
            //Console.WriteLine("Enter the string which you will want to change.");
            //var changeString = Console.ReadLine();
            //Console.WriteLine("Enter the line to which you want to change.");
            //var changedString = Console.ReadLine();

            var a = new ChangeTextAsync();
            a.ChangeFile(a.GetFilesWithTipe(directiry, ".txt")[0], "ll", "xxx");
            a.SaveLogFile();
            Console.ReadKey();
        }

       
    }
}
