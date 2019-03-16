using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace Firefly
{
    class Functions
    {
        Func<double, double, double> calculateFitness;
        double alpha, gamma, beta, min, max;
        int population, moveBest;
        bool modified, gammaRandom;
        public List<Bug> bugs = new List<Bug>();
        Random r = new Random();
        public Bug bestBug;

        public Functions(Func<double, double, double> calculateFitness, int population, double alpha, double gamma, double beta, double min, double max, int moveBest, bool modified, bool gammaRandom)
        {
            this.calculateFitness = calculateFitness;
            this.population = population;
            this.alpha = alpha;
            this.gamma = gamma;
            this.beta = beta;
            this.min = min;
            this.max = max;
            this.moveBest = moveBest;
            this.modified = modified;
            this.gammaRandom = gammaRandom;

        }

        public void createİnitialPopulation()
        {
            bugs.Clear();
            for (int i = 0; i < population; i++)
            {
                bugs.Add(new Bug(r.NextDouble() * (max - min) + min, r.NextDouble() * (max - min) + min, calculateFitness));
            }
            bestBug = bugs[0];
        }

        public void findBest()
        {
            Bug temp = bugs[0];
            for (int i = 1; i < bugs.Count; i++)
            {
                if (temp.fitness > bugs[i].fitness) temp = bugs[i];
            }
            if (bestBug.fitness > temp.fitness) bestBug = temp;
        }

        public void tryAllWay()
        {
            int counter = 0;

            if (gammaRandom)
                gamma = this.r.NextDouble() * 2;

            for (int i = 0; i < population; i++)
            {
                for (int j = 0; j < population; j++)
                {
                    if (bugs[i].fitness > bugs[j].fitness)
                    {
                        bugs[i] = moveBug(bugs[i], bugs[j]);
                        if (bestBug.fitness > bugs[i].fitness) bestBug = bugs[i];
                        counter++;
                    }
                }

                if (counter == 0 && moveBest == 1)
                {
                    bugs[i].x = this.r.NextDouble() * (max - min) + min;
                    bugs[i].y = this.r.NextDouble() * (max - min) + min;
                    bugs[i].fitness = calculateFitness(bugs[i].x, bugs[i].y);
                    if (bestBug.fitness > bugs[i].fitness) bestBug = bugs[i];
                }
                counter = 0;
            }
        }

        Bug moveBug(Bug first, Bug second)
        {
            double lightIntensity, lightIntensity2;
            double r = Math.Pow(first.x - second.x, 2) + Math.Pow(first.y - second.y, 2);
            if (modified)
            {
                //lightIntensity = (first.fitness) / (1 + gamma * r);
                // lightIntensity2 = (second.fitness) / (1 + gamma * r);
                lightIntensity = beta * Math.Exp(-1 * gamma * r);
                lightIntensity2 = beta * Math.Exp(-1 * gamma * r);
                first.x = (lightIntensity * first.x + lightIntensity2 * second.x) / (lightIntensity + lightIntensity2);
                first.y = (lightIntensity * first.y + lightIntensity2 * second.y) / (lightIntensity + lightIntensity2);
            }
            else
            {
                lightIntensity = beta * Math.Exp(-1 * gamma * r);
                first.x = first.x + lightIntensity * (second.x - first.x) + alpha * (this.r.NextDouble() - 0.5);
                first.y = first.y + lightIntensity * (second.y - first.y) + alpha * (this.r.NextDouble() - 0.5);
            }
            first.fitness = calculateFitness(first.x, first.y);
            return first;
        }
    }
}
