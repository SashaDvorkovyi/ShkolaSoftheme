using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car("nisan", "1999", "red");
            Console.WriteLine("{0}, {1}, {2}", car1.model, car1.year, car1.color);
            TuningAtelier.TuneCar("green", car1);
            Console.WriteLine("{0}, {1}, {2}", car1.model, car1.year, car1.color);
        }
    }
}
