using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firefly
{
    class Sphere
    {
        public static double min = -1000, max = 1000;
        public static double fitness(double x, double y)
        {
            return x * x + y * y;
        }
    }
}
