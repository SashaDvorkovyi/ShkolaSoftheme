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
            List<IUser> users = new List<IUser>();
            AuthenticatorNew authenticatorNew = new AuthenticatorNew();
            AuthenticatorNoisyUsers authenticatorNoisyUsers = new AuthenticatorNoisyUsers();
            bool exit = false;
            do
            {

                Console.WriteLine("If you want to login click: 'l'. If you want to create a user, click: 'c'");
                var loginOrCreate = (char)Console.ReadKey().Key;
                switch (loginOrCreate)
                {
                    case 'L':
                        Console.WriteLine();
                        string[] withdraw = { "Enter name of user or email end pres 'Enter' or 'exit' to end", "Enter passvord of user end pres 'Enter' or 'exit' to end" };
                        string[] inputs = new string[withdraw.Length];
                        for (var i = 0; i < inputs.Length; i++)
                        {
                            Console.WriteLine(withdraw[i]);
                            var input = Console.ReadLine();
                            if (string.Compare(input, "exit") == 0)
                            {
                                exit = true;
                                break;
                            }
                            else
                            {
                                inputs[i] = input;
                            }
                        }
                        if (inputs[1] != null)
                        {
                            User userA = new User(inputs[0], inputs[1], null);
                            User userB = new User(null, inputs[1], inputs[0]);
                            if (authenticatorNoisyUsers.AuthenticateUser(ref users, userA) != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(authenticatorNoisyUsers.AuthenticateUser(ref users, userA).GetFullInfo());
                                Console.ResetColor();
                            }
                            else if(authenticatorNoisyUsers.AuthenticateUser(ref users, userB) != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(authenticatorNoisyUsers.AuthenticateUser(ref users, userA).GetFullInfo());
                                Console.ResetColor();
                            }

                        }
                        break;
                    case 'C':

                        Console.WriteLine();
                        string[] withdraw1 = { "Enter name of user end pres 'Enter' or 'exit' to end", "Enter email of user end pres 'Enter' or 'exit' to end", "Enter passvord of user end pres 'Enter' or 'exit' to end" };
                        string[] inputs1 = new string[withdraw1.Length];
                        for (var i=0; i<inputs1.Length; i++)
                        {
                            Console.WriteLine(withdraw1[i]);
                            var input = Console.ReadLine();
                            if( string.Compare(input, "exit")==0)
                            {
                                exit = true;
                                break;
                            }
                            else
                            {
                                inputs1[i] = input;
                            }
                        }
                        if (inputs1[2] != null)
                        {
                            User user = new User(inputs1[0], inputs1[2], inputs1[1]);
                            if (authenticatorNew.AuthenticateUser(ref users, user) == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(user.GetFullInfo());
                                Console.ResetColor();
                            }

                        }
                        break;
                }

            }
            while (exit!=true);

        }
    }
}
