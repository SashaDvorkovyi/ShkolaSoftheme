using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUser
{
    class Program
    {
        static void Main(string[] args)
        {
            Authenticator authenticator = new Authenticator();

            do
            {
                Console.WriteLine("If you want to login click: 'l'. If you want to create a user, click: 'c'");
                var loginOrCreate = (char)Console.ReadKey().Key;
                switch (loginOrCreate)
                {
                    case 'L':
                        Console.WriteLine();
                        Console.WriteLine("Enter name of user or email of user end pres 'Enter'");
                        var nameOrEmail = Console.ReadLine();
                        Console.WriteLine("Enter passvord of user end pres 'Enter'");
                        var password1 = Console.ReadLine();
                        User userA = new User(nameOrEmail, password1, null);
                        User userB = new User(null, password1, nameOrEmail);
                        if (authenticator.AuthenticateUser((IUser)userA) != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(authenticator.AuthenticateUser((IUser)userA).GetFullInfo());
                            Console.ResetColor();
                        }
                        else if (authenticator.AuthenticateUser((IUser)userB) != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(authenticator.AuthenticateUser((IUser)userB).GetFullInfo());
                            Console.ResetColor();
                        }
                        break;
                    case 'C':
                        Console.WriteLine();
                        Console.WriteLine("Enter name of user end pres 'Enter'");
                        var name = Console.ReadLine();
                        Console.WriteLine("Enter email of user end pres 'Enter'");
                        var email = Console.ReadLine();
                        Console.WriteLine("Enter passvord of user end pres 'Enter'");
                        var password = Console.ReadLine();
                        if (name != "" && name != " " && email != "" && email != " " && password != "" && password != " ")
                        {
                            User user = new User(name, password, email);
                            authenticator.AddUser(user);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(user.GetFullInfo());
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("You entered the wrong data");
                        }
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("If you want to exit click: 'Escape'. If you want to contine click something else.");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);

        }
    }
}
