using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    public class Car
    {
        public string model { get; private set; }
        public string year { get; private set; }
        public string color { get; set; }

        public Car(string model, string year, string color)
        {
            this.model = model;
            this.year = year;
            this.color = color;
        }

    }
}
