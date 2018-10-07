using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03
{
    public class SimulatedAnnealingAlgorithm : IAlgorithm
    {
        public double Alpha { get; }
        public double Temperature { get; private set; }

        private readonly Random _random = new Random();

        public SimulatedAnnealingAlgorithm(double alpha = 0.99)
        {
            Alpha = alpha;
            Temperature = 2000;
        }

        public List<Individual> GeneratePopulation(Population population)
        {
            Individual result;

            var x = _random.NextNormalDistribution(population.Dimensions, population.StandardDeviation, population.Mean);
            Enumerable.Range(0, population.Dimensions)
                .ForEach(dimension => x[dimension] += population.BestIndividual[dimension]); // translate by current best individual

            var newBest = new Individual(x, population.OptimizationFunction.Calculate(x));

            if ((population.OptimizationTarget == OptimizationTarget.Maximum && newBest.Result > population.BestIndividual.Result)
                || (population.OptimizationTarget == OptimizationTarget.Minimum && newBest.Result < population.BestIndividual.Result))
            {
                result = newBest;
            }
            else
            {
                result = ShouldMoveToWorseSolution(population.BestIndividual, newBest) ? newBest : population.BestIndividual;
            }

            Temperature *= Alpha;
            return new List<Individual> { result };
        }

        private bool ShouldMoveToWorseSolution(Individual old, Individual @new)
        {
            double r = _random.NextDouble();
            double delta = @new.Result - old.Result;

            return r < Math.Pow(Math.E, -delta / Temperature);
        }
    }
}
