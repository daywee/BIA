using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;

namespace Lesson06.OptimizationAlgorithms
{
    public class SimulatedAnnealingAlgorithm : IAlgorithm<Individual>
    {
        public double Alpha { get; }
        public double Temperature { get; private set; }
        public int MaxPopulation { get; } = 1;

        private readonly Random _random = new Random();

        public SimulatedAnnealingAlgorithm(double alpha = 0.99)
        {
            Alpha = alpha;
            Temperature = 2000;
        }

        public List<Individual> SeedPopulation(Population<Individual> population)
        {
            return new List<Individual> { population.GetRandomIndividual() };
        }

        public List<Individual> GeneratePopulation(Population<Individual> population)
        {
            Individual result;

            var x = new Vector(_random.NextNormalDistribution(population.Dimensions, population.StandardDeviation, population.Mean));
            x += population.BestIndividual.Position; // translate by current best individual

            var newBest = new Individual(x);
            newBest.ApplyBounds(population.OptimizationFunction, _random);
            newBest.CalculateCost(population.OptimizationFunction);

            if ((population.OptimizationTarget == OptimizationTarget.Maximum && newBest.Cost > population.BestIndividual.Cost)
                || (population.OptimizationTarget == OptimizationTarget.Minimum && newBest.Cost < population.BestIndividual.Cost))
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
            double delta = @new.Cost - old.Cost;

            return r < Math.Pow(Math.E, -delta / Temperature);
        }
    }
}
