using System;


namespace ChangeText
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pleas enter the all name of directiry");
            var directory = Console.ReadLine();
            Console.WriteLine("Pleas enter the tipe of file. Exemple: \".txt\"");
            var ditiofFile = Console.ReadLine();
            Console.WriteLine("Enter the expression will be replaced.");
            var expression1 = Console.ReadLine();
            Console.WriteLine("Enter an expression that will replaces the old");
            var expression2 = Console.ReadLine();

            var a = new ChangeTextAsync();
            a.ParallelCoincidencesSearchAndChangeFiles(directory, ditiofFile, expression1, expression2);
            a.SaveLogFile();

            Console.WriteLine("The program has completed execution. Now you can close it!!!");
            Console.ReadKey();
        } 
    }
}
