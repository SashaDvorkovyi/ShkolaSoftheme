using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var asm = Assembly.LoadFrom(@"D:\Git\CSharpHW\lesson 29\ConsoleApp1\ConsoleApp1\bin\Debug\ConsoleApp1.exe");

            var t = asm.GetType("ConsoleApp1.Program", true, true);
            Console.WriteLine("Type: {0}", t);

            var typeInfo = t.GetTypeInfo();
            Console.WriteLine("Type Info: {0}", typeInfo);

            var methods = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Static) ;

            foreach (var method in methods)
            {
                Console.WriteLine("Method: {0}", method);
            }

            // take method Multiply
            var methodMultiply = t.GetMethod("Multiply", BindingFlags.NonPublic | BindingFlags.Static);

            // invoke method
            if (methodMultiply != null)
            {
                var result = methodMultiply.Invoke(null, new object[] { 6, 100 });
                Console.WriteLine("Multiply: {0}", result);
            }

            // take method GetPi
            var methodGetPi = t.GetMethod("GetPi", BindingFlags.NonPublic | BindingFlags.Static);

            // invoke method
            if (methodGetPi != null)
            {
                var result = methodGetPi.Invoke(null, new object[] { });
                Console.WriteLine("GetPi {0}", result);
            }
        }
    }
}
