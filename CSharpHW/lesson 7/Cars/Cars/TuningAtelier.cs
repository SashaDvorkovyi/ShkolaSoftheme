using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    static class TuningAtelier
    {
        public static void TuneCar(string color, Car obj)
        {
            obj.color = color;
        }
    }
}
