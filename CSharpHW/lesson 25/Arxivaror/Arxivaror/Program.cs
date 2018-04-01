using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Threading;


namespace Arxivaror
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pleas enter the ol name of directiry");
            var directiry = new DirectoryInfo(Console.ReadLine());
            var listOfFiles=GeaAllFiles(directiry);

            Semaphore semaphore = new Semaphore(3, 3);

            foreach (var fileName in listOfFiles)
            {
                var thread = new Thread(() => 
                {
                    ReadAndWriteToZip(fileName, semaphore);
                });

                thread.Start();
            }
        }

        public static void ReadAndWriteToZip(string fileName, Semaphore semaphore)
        {
            semaphore.WaitOne();
            using (FileStream fs = new FileStream(fileName.Substring(0, fileName.LastIndexOf('.')) + ".zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    arch.CreateEntryFromFile(fileName, fileName.Substring(fileName.LastIndexOf('\\')));
                }
            }
            semaphore.Release();
        }

        public static List<string> GeaAllFiles(DirectoryInfo directiry)
        {
            var list = new List<string>();
            foreach (var dir in directiry.GetDirectories())
            {
                GeaAllFiles(new DirectoryInfo(dir.FullName), ref list);
            }
            foreach (var file in directiry.GetFiles())
            {
                list.Add(file.FullName);
            }
            return list;
        }

        public static List<string> GeaAllFiles(DirectoryInfo directiry, ref List<string> list)
        {
            foreach (var dir in directiry.GetDirectories())
            {
                GeaAllFiles(new DirectoryInfo(dir.FullName), ref list);
            }
            foreach (var file in directiry.GetFiles())
            {
                if (string.Equals(".zip", file.Name.Substring(file.Name.LastIndexOf('.'))))
                {
                    continue;
                }
                list.Add(file.FullName);
            }
            return list;
        }
    }
}
