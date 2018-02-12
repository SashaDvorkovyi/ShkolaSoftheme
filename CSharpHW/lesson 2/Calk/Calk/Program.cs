using System;


namespace Calk
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] memory = new string[5, 5];
            for (var i = 0; i <= 4; i++)
            {
                for (var t = 0; t <= 4; t++)
                {
                    memory[t, i] = " ";
                }
                memory[3, i] = "=";
            }

            int number = 0;

            do
            {
                Console.WriteLine("Please write the first number and click enter");
                bool wrong = false;
                double answer = 0;
                double value;
                string second = null;
                string first = Console.ReadLine();
                double firstOrerant;
                if (double.TryParse(first, out value))
                {
                    firstOrerant = double.Parse(first);
                }
                else
                {
                    Console.WriteLine("Sorry, but you were wrong");
                    continue;
                }
                Console.WriteLine("Please write the operation and click enter");
                Console.WriteLine("You can enter such operators: sqrt, sin, cos, log, -, +, *, /, ^.");
                string operation = Console.ReadLine();
                if (string.Equals(operation, "sqrt") || string.Equals(operation, "sin") || string.Equals(operation, "cos") || string.Equals(operation, "log"))
                {
                    if (operation == "sqrt")
                    {
                        answer = Math.Sqrt(firstOrerant);
                    }
                    if (operation == "sin")
                    {
                        answer = Math.Sin(firstOrerant);
                    }
                    if (operation == "cos")
                    {
                        answer = Math.Cos(firstOrerant);
                    }
                    if (operation == "log")
                    {
                        answer = Math.Log(firstOrerant);
                    }
                }
                else if (string.Equals(operation, "-") || string.Equals(operation, "+") || string.Equals(operation, "*") || string.Equals(operation, "/") || string.Equals(operation, "^"))
                {
                    Console.WriteLine("Please write the sekond number and click enter");
                    second = Console.ReadLine();
                    double secondOrerant;
                    if (double.TryParse(second, out value))
                    {
                        secondOrerant = double.Parse(second);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, but you were wrong");
                        continue;
                        wrong = true;
                    }
                    if (operation == "+")
                    {
                        answer = firstOrerant + secondOrerant;
                    }
                    else if (operation == "-")
                    {
                        answer = firstOrerant - secondOrerant;
                    }
                    else if (operation == "*")
                    {
                        answer = firstOrerant * secondOrerant;
                    }
                    else if (operation == "/")
                    {
                        answer = firstOrerant / secondOrerant;
                    }
                    else if (operation == "^")
                    {
                        answer = Math.Pow(firstOrerant, secondOrerant);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry, but you were wrong");
                    continue;
                    wrong = true;
                }
                Console.WriteLine("Ansver is");
                Console.WriteLine(Math.Round(answer,2).ToString());
                Console.WriteLine();
                Console.WriteLine("Click 'c' to clear informotion");
                Console.WriteLine("Click 'm' to output the last five operations or something else to contine");
                string c_or_p = Console.ReadLine();

                if (wrong != true)
                {
                    if ((memory[0, number] == " ") && (second != null))
                    {
                        memory[0, number] = first; memory[1, number] = operation; memory[2, number] = second; memory[4, number] = Math.Round(answer, 2).ToString();
                    }
                    else if ((memory[0, number] == " ") && (second == null))
                    {
                        memory[0, number] = first; memory[1, number] = operation; memory[4, number] = Math.Round(answer, 2).ToString();
                    }
                    else if ((number == 4) && (memory[0, number] != " "))
                    {
                        for (var i = 1; i <= 3; i++)
                        {
                            for (var t = 0; t <= 4; t++)
                            {
                                memory[t, i - 1] = memory[t, i];
                            }
                        }
                        memory[0, 4] = first; memory[1, 4] = operation; memory[2, 4] = second; memory[4, 4] = Math.Round(answer, 2).ToString();
                    }
                    if (number < 4)
                    {
                        number++;
                    }
                }
                if (string.Equals(c_or_p, "m"))
                {
                    for (var i = 0; i <= 4; i++)
                    {
                        for (var t = 0; t <= 4; t++)
                        {
                            Console.Write(" " + memory[t, i]);
                        }
                        Console.WriteLine();
                    }
                }
                if (string.Equals(c_or_p, "c"))
                {
                    Console.Clear();
                }

                Console.WriteLine("Press escape to exit or something else to contine");

            }
            while(Console.ReadKey().Key != ConsoleKey.Escape) ;

        }
    }
}
