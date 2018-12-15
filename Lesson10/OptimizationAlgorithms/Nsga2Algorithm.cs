using System;
using Shared.TestFunctions;
using System.Collections.Generic;
using System.Linq;

namespace Lesson10.OptimizationAlgorithms
{
    public class Nsga2Algorithm : IAlgorithm
    {
        public int MaxPopulation { get; } = 10;

        private readonly Random _random = new Random();

        public List<Individual> SeedPopulation(Population population)
        {
            return Enumerable.Range(0, MaxPopulation)
                .Select(_ => CreateRandomIndividual(population.OptimizationFunction1, population.OptimizationFunction2, population.Dimension))
                .ToList();
        }

        public List<Individual> GeneratePopulation(Population population)
        {
            throw new NotImplementedException();
        }

        private Individual CreateRandomIndividual(OneDimensionFunctionBase optimizationFunction1, OneDimensionFunctionBase optimizationFunction2, int dimension)
        {
            var min = optimizationFunction1.MinX;
            var max = optimizationFunction2.MaxX;
            var interval = Math.Abs(max - min);

            var randomCoordinates = _random.NextDouble() * interval - max;

            var newIndividual = new Individual(randomCoordinates);
            newIndividual.CalculateCost(optimizationFunction1, optimizationFunction2);

            return newIndividual;
        }
    }
}
