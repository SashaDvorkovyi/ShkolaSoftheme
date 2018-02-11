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
            var car1 = CarConstructor.Construct("blue", "v8", "mechanics");
            var engine1 = new Engine("v6");
            var color1 = new Color("green");
            var transmission1 = new Transmission("auto");
            var car2 = CarConstructor.Construct(color1, engine1, transmission1);
            Console.WriteLine("Car1: " + car1.color.color + ", " + car1.engine.engineName + ", " + car1.transmission.transmission);
            Console.WriteLine("Car2: " + car2.color.color + ", " + car2.engine.engineName + ", " + car2.transmission.transmission);

            car1 = CarConstructor.Reconstruct(car1, "v4 turbo");
            Console.WriteLine("Car1: " + car1.color.color + ", " + car1.engine.engineName + ", " + car1.transmission.transmission);
            car2 = CarConstructor.Reconstruct(car2, "v2 turbo");
            Console.WriteLine("Car2: " + car2.color.color + ", " + car2.engine.engineName + ", " + car2.transmission.transmission);

            Console.ReadKey();
        }
    }
}
