using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConstruct
{
    class Program
    {
        static void Main(string[] args)
        {

            var car1 = CarConstructor.Construct(Color.black, Engine.М_271, Transmission.mechanics);

            Console.WriteLine("Car1: " + car1.color.ToString() + ", " + car1.engine.ToString() + ", " + car1.transmission.ToString());
            car1 = CarConstructor.Reconstruct(car1, Engine.М_272);

            Console.WriteLine("Car1: " + car1.color.ToString() + ", " + car1.engine.ToString() + ", " + car1.transmission.ToString());
            car1 = CarConstructor.Reconstruct(car1, Color.red, Engine.ОМ_611);

            Console.WriteLine("Car1: " + car1.color.ToString() + ", " + car1.engine.ToString() + ", " + car1.transmission.ToString());
            Console.ReadKey();
        }
    }
}
