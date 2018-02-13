using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConstruct
{
    public struct Car
    {
        public Color color { get; }
        public Engine engine { get; }
        public Transmission transmission { get; }

        public Car(Color color, Engine engine, Transmission transmission)
        {
            this.color = color;
            this.engine = engine;
            this.transmission = transmission;
        }

    }
}
