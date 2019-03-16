using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firefly
{
    public class Bug
    {
        public double x, y, fitness;
        public Bug(double x, double y, Func<double, double, double> calculateFitness)
        {
            this.x = x; this.y = y; this.fitness = calculateFitness(x, y);
        }
    }
}
