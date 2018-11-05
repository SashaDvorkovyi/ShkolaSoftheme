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
            Console.WriteLine("Pleas enter the all name of directiry");
            var directiry = new DirectoryInfo(Console.ReadLine());
            Console.WriteLine(@"Pleas enter 'r' if you want want to archive files, or 'z' if you want extract from archive");
            var key = Console.ReadKey();
            if (key.KeyChar == 'r')
            {
                try
                {
                    var listOfFiles = GetAllFilesWithOutZip(directiry);
                    WriteToZipInThreeThrids(listOfFiles);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("You have entered the wrong folder name");
                }

            }
            else if (key.KeyChar == 'z')
            {
                try
                {
                    var listOfZip = GetAllZipFile(directiry);
                    UnZipInThreeThrids(listOfZip);
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("You have entered the wrong folder name");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Already done. You can close the program.");
            Console.ReadKey();
        }

        public static void WriteToZipInThreeThrids(List<string> listOfFiles)
        {
            Thread[] treads = new Thread[] {
                new Thread(new ParameterizedThreadStart(WriteToZip)),
                new Thread(new ParameterizedThreadStart(WriteToZip)),
                new Thread(new ParameterizedThreadStart(WriteToZip))
            };

            for (var fileNumeNumber = 0; fileNumeNumber < listOfFiles.Count; fileNumeNumber++)
            {
                if (fileNumeNumber < treads.Length)
                {
                    treads[fileNumeNumber].Start(listOfFiles[fileNumeNumber]);
                    continue;
                }
                var i = 0;
                while (true)
                {
                    if (string.Equals(treads[i].ThreadState.ToString(), "Stopped"))
                    {
                        treads[i] = new Thread(new ParameterizedThreadStart(WriteToZip));
                        treads[i].Start(listOfFiles[fileNumeNumber]);
                        break;
                    }
                    i = (i < 3) ? i++ : i = 0;
                }
            }
            foreach (var thread in treads)
            {
                thread.Join();
            }
        }

        public static void UnZipInThreeThrids(List<string> listOfZip)
        {
            Thread[] treads = new Thread[] {
                new Thread(new ParameterizedThreadStart(UnZip)),
                new Thread(new ParameterizedThreadStart(UnZip)),
                new Thread(new ParameterizedThreadStart(UnZip))
            };

            for (var fileNumeNumber = 0; fileNumeNumber < listOfZip.Count; fileNumeNumber++)
            {
                if (fileNumeNumber < treads.Length)
                {
                    treads[fileNumeNumber].Start(listOfZip[fileNumeNumber]);
                    continue;
                }
                var i = 0;
                while (true)
                {
                    if (string.Equals(treads[i].ThreadState.ToString(), "Stopped"))
                    {
                        treads[i] = new Thread(new ParameterizedThreadStart(UnZip));
                        treads[i].Start(listOfZip[fileNumeNumber]);
                        break;
                    }
                    i = (i < 3) ? i++ : i = 0;
                }
            }
            foreach (var thread in treads)
            {
                thread.Join();
            }
        }

        public static void WriteToZip(object fileNameObj)
        {
            var fileName = fileNameObj.ToString();
            using (FileStream fs = new FileStream(fileName.Substring(0, fileName.LastIndexOf('.')) + ".zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    arch.CreateEntryFromFile(fileName, (fileName.Substring(fileName.LastIndexOf('\\')+1)));
                }
            }
        }

        public static void UnZip(object fileNameObj)
        {
            var fileName = fileNameObj.ToString();
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    using (ZipArchive arch = new ZipArchive(fs))
                    {
                        arch.ExtractToDirectory(fileName.Substring(0, fileName.LastIndexOf("\\")));
                    }
                }
                catch (IOException)
                {
                    Console.WriteLine("A file with that name \"{0}\" already exists!", fileName);
                }
            }
        }

        public static List<string> GetAllFilesWithOutZip(DirectoryInfo directiry)
        {
            var list = new List<string>();
            foreach (var file in directiry.GetFiles("*", SearchOption.AllDirectories))
            {
                if (string.Equals(".zip", file.Name.Substring(file.Name.LastIndexOf('.'))))
                {
                    continue;
                }
                list.Add(file.FullName);
            }
            return list;
        }

        public static List<string> GetAllZipFile(DirectoryInfo directiry)
        {
            var list = new List<string>();
            foreach (var file in directiry.GetFiles("*.zip", SearchOption.AllDirectories))
            {
                list.Add(file.FullName);
            }
            return list;
        }
    }
}
