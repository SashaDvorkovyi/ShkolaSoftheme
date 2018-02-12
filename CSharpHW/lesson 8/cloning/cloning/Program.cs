using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cloning
{
    class Program
    {
        static void Main(string[] args)
        {
            UserReferense ob1 = new UserReferense("Dima", "Romanov");
            ob1.show();
            var ob2 = ob1.clone();
            ob1.show();
            ob2.show();
            UserReferense ob3 = new UserReferense("Roma", "Korm");
            ob3.show();
            Console.WriteLine();

            //var falseID = IDNumber.idTake();

            UserStruct struct1 = new UserStruct("Dinis","Volia");
            //UserStruct struct2 = struct1;
            var struct2 = struct1.clone();
            Console.WriteLine("UserStruct: First name: {0}. Last name: {1}. ID: {2}", struct1.firstName, struct1.lastName, struct1.id);
            Console.WriteLine("UserStruct: First name: {0}. Last name: {1}. ID: {2}", struct2.firstName, struct2.lastName, struct2.id);
            Console.ReadKey();

        }
    }
}
