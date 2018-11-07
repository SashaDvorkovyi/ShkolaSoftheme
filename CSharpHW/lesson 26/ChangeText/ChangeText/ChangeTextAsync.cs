using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Concurrent;

namespace ChangeText
{
    public class ChangeTextAsync
    {
        public ConcurrentDictionary<string, Dictionary<int, string[]>> LogDictionary { get; private set; }

        public ChangeTextAsync()
        {
            LogDictionary = new ConcurrentDictionary<string, Dictionary<int, string[]>>();
        }

        public void ChangeFile(string fullNameOfFile, string expressionSearched, string newValue)
        {
            var coincidences = CoincidencesSearch(fullNameOfFile, expressionSearched);

            if (coincidences == null)
            {
                return;
            }
            else
            {
                var newTextInList = new List<string>();

                LogDictionary.TryAdd(fullNameOfFile, new Dictionary<int, string[]>());
                using (StreamReader stream = new StreamReader(fullNameOfFile))
                {
                    var line = stream.ReadLine();
                    var numberOfLine = 0;
                    while (line != null)
                    {
                        if (coincidences.ContainsKey(numberOfLine))
                        {
                            LogDictionary[fullNameOfFile].Add(numberOfLine, new string[2]);
                            LogDictionary[fullNameOfFile][numberOfLine][0] = line;
                            var newLine = default(string);
                            var smallerNumber = 0;
                            foreach (var largerNumber in coincidences[numberOfLine])
                            {
                                newLine += line.Substring(smallerNumber, largerNumber+1 - smallerNumber);
                                newLine += newValue;
                                smallerNumber = +largerNumber+1 + expressionSearched.Length;
                            }
                            newLine += line.Substring(smallerNumber, line.Length - smallerNumber);
                            LogDictionary[fullNameOfFile][numberOfLine][1] = newLine;
                            newTextInList.Add(newLine);
                        }
                        else
                        {
                            newTextInList.Add(line);
                        }
                        line = stream.ReadLine();
                        numberOfLine += 1;
                    }
                }
                using(StreamWriter strem=new StreamWriter(fullNameOfFile,  false, Encoding.Default))
                {
                    foreach(var item in newTextInList)
                    {
                        strem.WriteLine(item);
                    }
                }
            }
        }

        public void SaveLogFile()
        {
            using(var fileStr = new FileStream("D:\\logFile.txt", FileMode.OpenOrCreate))
            {
                using (var strem = new StreamWriter(fileStr, Encoding.Default))
                {
                    foreach (var file in LogDictionary)
                    {
                        strem.WriteLine("Full file name is: "+file.Key);
                        foreach (var line in file.Value)
                        {
                            strem.WriteLine("   Nuber of line is:   " + line.Key.ToString());
                            strem.WriteLine("      Before line was: " + line.Value[0]);
                            strem.WriteLine("      Now line is:       " + line.Value[1]);
                        }
                    }
                }   
            }
        }

        public List<string> GetFilesWithTipe(DirectoryInfo directiry, string tipeOfFile)
        {
            var list = new List<string>();
            foreach (var file in directiry.GetFiles("*" + tipeOfFile, SearchOption.AllDirectories))
            {
                list.Add(file.FullName);
            }
            return list;
        }

        public Dictionary<int, List<int>> CoincidencesSearch(string fullNameOfFile, string expressionSearched)
        {
            var result = new Dictionary<int, List<int>>();
            var numberOfLine = 0;
            var hit = false;

            using (StreamReader stream = new StreamReader(fullNameOfFile))
            {
                var numberElement = 0;
                var line = stream.ReadLine();

                while (line != null)
                {
                    for (var i = 0; i < line.Length; i++)
                    {
                        if (expressionSearched[numberElement] == line[i])
                        {
                            numberElement++;
                            if (numberElement == expressionSearched.Length)
                            {
                                if (!result.ContainsKey(numberOfLine))
                                {
                                    result.Add(numberOfLine, new List<int>() { i - expressionSearched.Length });
                                    hit = true;
                                }
                                else
                                {
                                    result[numberOfLine].Add(i - expressionSearched.Length);
                                    hit = true;
                                }
                                numberElement = 0;
                            }
                        }
                        else
                        {
                            numberElement = 0;
                        }
                    }
                    line = stream.ReadLine();
                    numberOfLine++;
                }
            }
            return hit == true ? result : null;
        }
    }
}
