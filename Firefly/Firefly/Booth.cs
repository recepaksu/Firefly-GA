using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firefly
{
    class Booth
    {
        public static double min = -10, max = 10;
        public static double fitness(double x, double y)
        {
            return (x + 2 * y - 7) * (x + 2 * y - 7) + (2 * x + y - 5) * (2 * x + y - 5);
        }
    }
}
