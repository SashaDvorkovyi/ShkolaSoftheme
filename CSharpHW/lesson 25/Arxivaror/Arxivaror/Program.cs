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
            var listOfFiles = GeaAllFiles(directiry);


            Thread[] treads = new Thread[] {
                new Thread(new ParameterizedThreadStart(ReadAndWriteToZip)),
                new Thread(new ParameterizedThreadStart(ReadAndWriteToZip)),
                new Thread(new ParameterizedThreadStart(ReadAndWriteToZip))
            };

            for (var fileNumeNumber=0; fileNumeNumber< listOfFiles.Count; fileNumeNumber++)
            {
                if (fileNumeNumber < treads.Length)
                {
                    treads[fileNumeNumber].Start(listOfFiles[fileNumeNumber]);
                    continue;
                }
                var i = 0;
                while(true)
                {
                    if(string.Equals(treads[i].ThreadState.ToString(), "Stopped"))
                    {
                        treads[i] = new Thread(new ParameterizedThreadStart(ReadAndWriteToZip));
                        treads[i].Start(listOfFiles[fileNumeNumber]);
                        break;
                    }
                    i++;
                    if (i == 3)
                    { i = 0; }
                }

            }
            foreach(var thread in treads)
            {
                thread.Join();
            }
            Console.WriteLine("Already done. You can close the program.");
            Console.ReadKey();
        }

        public static void ReadAndWriteToZip(object fileNameObj)
        {
            var fileName = fileNameObj.ToString();
            using (FileStream fs = new FileStream(fileName.Substring(0, fileName.LastIndexOf('.')) + ".zip", FileMode.OpenOrCreate))
            {
                using (ZipArchive arch = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    arch.CreateEntryFromFile(fileName, fileName.Substring(fileName.LastIndexOf('\\')));
                }
            }
        }

        public static List<string> GeaAllFiles(DirectoryInfo directiry)
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
    }
}
