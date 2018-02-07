using System;

namespace figures
{
    class Program
    {
        static void Main(string[] args)
        {
            var i= 0;
            while(i<1)
                {
                Console.WriteLine(@"what shape do you want to draw?
if it's triangle click - tr
if it's square click - sq
if it's rhombusclick - rh");
                string figura = Console.ReadLine();
                Console.WriteLine("Enter the length of the diagonal");
                string diagonalString = Console.ReadLine();
                int diagonal = 0;
                if (int.TryParse(diagonalString, out int resalt))
                {
                    diagonal = resalt;
                }
                else
                {
                    Console.WriteLine("Sorry, but you were wrong");
                }

                switch (figura)
                {
                    case "tr":
                        for(int t=1; t<= diagonal; t++)
                        {
                            for (int n = 1; n <= t; n++)
                            {
                                Console.Write("* ");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "sq":
                        for (int t = 1; t <= diagonal; t++)
                        {
                            for (int n = 1; n <= diagonal; n++)
                            {
                                Console.Write("* ");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case "rh":
                        for (int t = diagonal; t > 0; t--)
                        {
                            for (int n = 1; n < t; n++)
                            {
                                Console.Write("  ");
                            }
                            for (int n = 1; n <= ((diagonal-t)*2+1); n++)
                            {
                                Console.Write("* ");
                            }
                            Console.WriteLine();
                        }
                        for (int t = 1; t < diagonal; t++)
                        {
                            for (int n = 1; n <= t; n++)
                            {
                                Console.Write("  ");
                            }
                            for (int n = 1; n <= ((diagonal  - t) * 2 - 1); n++)
                            {
                                Console.Write("* ");
                            }
                            Console.WriteLine();
                        }
                        break;
                    default:
                        Console.WriteLine("Sorry, but you were wrong");
                        break;
                }

                Console.ReadKey();
            }

        }
    }
}