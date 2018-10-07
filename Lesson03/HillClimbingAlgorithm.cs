using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03
{
    public class HillClimbingAlgorithm : IAlgorithm
    {
        private readonly Random _random = new Random();

        public int MaxPopulation { get; } = 50;

        public List<Individual> GeneratePopulation(Population population)
        {
            return Enumerable.Range(0, population.MaxPopulationCount)
                .Select(_ =>
                {
                    var x = _random.NextNormalDistribution(population.Dimensions, population.StandardDeviation, population.Mean);
                    Enumerable.Range(0, population.Dimensions)
                        .ForEach(dimension => x[dimension] += population.BestIndividual[dimension]); // translate by current best individual

                    return new Individual(x, population.OptimizationFunction.Calculate(x));
                })
                .ToList();
        }
    }
}
