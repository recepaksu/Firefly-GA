using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firefly
{
    class Himmelblau
    {
        public static double min = -5, max = 5;
        public static double fitness(double x, double y)
        {
            return (x * x + y - 11) * (x * x + y - 11) + (x + y * y - 7) * (x + y * y - 7);
        }
    }
}
