using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConstruct
{
    public static class CarConstructor
    {
        public static Car Construct(string color, string engine, string transmission)
        {
            Car car = new Car(new Color(color), new Engine(engine), new Transmission(transmission));
            return car;
        }
        public static Car Construct(Color color, Engine engine, Transmission transmission)
        {
            Car car = new Car(color, engine, transmission);
            return car;
        }
        public static Car Reconstruct(Car car, string engine)
        {
            Car carNew = new Car(car.color, new Engine(engine), car.transmission);
            return carNew;
        }
        public static Car Reconstruct(Car car, Engine engine)
        {
            Car carNew = new Car(car.color, engine, car.transmission);
            return carNew;
        }
    }
}
