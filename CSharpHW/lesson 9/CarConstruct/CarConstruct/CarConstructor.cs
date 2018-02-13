using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConstruct
{
    public static class CarConstructor
    {

        public static Car Construct(Color color, Engine engine, Transmission transmission)
        {
            Car car = new Car(color, engine, transmission);
            return car;
        }

        public static Car Reconstruct(Car car, Engine engine)
        {
            Car newCar = new Car(car.color, engine, car.transmission);
            return newCar;
        }
        public static Car Reconstruct(Car car, Color color, Engine engine)
        {
            Car newCar = new Car(color, engine, car.transmission);
            return newCar;
        }
    }
}
