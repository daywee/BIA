using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson09.OptimizationAlgorithms
{
    public class EvolutionaryStrategyAlgorithmMuPlusLambda : IAlgorithm<Individual>
    {
        public int MaxPopulation { get; } = 20;

        private const double OneFifth = 1 / 5d;
        private const double StockingMutation = 0.817;

        private double _sigma = 1;
        private readonly Random _random = new Random();

        public List<Individual> SeedPopulation(Population<Individual> population)
        {
            _sigma = 1;
            return Enumerable.Range(0, MaxPopulation)
                .Select(_ => population.GetRandomIndividual())
                .ToList();
        }

        public List<Individual> GeneratePopulation(Population<Individual> population)
        {
            var children = population.CurrentPopulation
                .Select(e => new Individual(e.Position + new Vector(_random.NextNormalDistribution(population.Dimensions, _sigma))))
                .ToList();

            children.ForEach(e =>
            {
                e.ApplyBounds(population.OptimizationFunction, _random);
                e.CalculateCost(population.OptimizationFunction);
            });

            var newPopulation = population.CurrentPopulation
                .Concat(children)
                .OrderBy(e => e.Cost)
                .Take(MaxPopulation)
                .ToList();

            int successfulMutations = newPopulation.Intersect(children).Count();
            UpdateSigma(successfulMutations);

            return newPopulation;
        }

        // rule of one fifth
        private void UpdateSigma(int successfulMutations)
        {
            double rate = (double)successfulMutations / MaxPopulation;

            if (rate > OneFifth)
                _sigma /= StockingMutation;
            else if (rate < OneFifth)
                _sigma *= StockingMutation;
        }
    }
}
