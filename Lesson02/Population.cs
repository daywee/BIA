using Shared.ExtensionMethods;
using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson02
{
    public class Population
    {
        public int Dimensions { get; }
        public int PopulationCount { get; }
        public List<Individual> CurrentPopulation { get; private set; }
        public int Generation { get; private set; }

        private readonly FunctionBase _optimizationFunction;
        private readonly Random _random = new Random();
        private Individual _bestIndividual;

        public Population(FunctionBase optimizationFunction, int populationCount, int dimensions)
        {
            _optimizationFunction = optimizationFunction;
            PopulationCount = populationCount;
            Dimensions = dimensions;
            _bestIndividual = GetRandomIndividual();
        }

        public void Evolve()
        {
            GeneratePopulation();
            SetBestIndividual();
            Generation++;
        }

        private void GeneratePopulation()
        {
            CurrentPopulation = Enumerable.Range(0, PopulationCount)
                .Select(_ =>
                {
                    var (x1, x2) = _random.NextNormalDistribution2D();

                    // translate by current best individual
                    x1 += _bestIndividual[0];
                    x2 += _bestIndividual[1];

                    return new Individual(Dimensions) { [0] = x1, [1] = x2 };
                })
                .ToList();
        }

        private void SetBestIndividual()
        {
            Individual bestIndividual = CurrentPopulation.First();
            double bestValue = _optimizationFunction.Calculate(bestIndividual[0], bestIndividual[1]);

            for (int i = 1; i < PopulationCount; i++)
            {
                Individual currentIndividual = CurrentPopulation[i];
                double currentValue = _optimizationFunction.Calculate(currentIndividual[0], currentIndividual[1]);

                if (currentValue > bestValue)
                {
                    bestIndividual = currentIndividual;
                    bestValue = currentValue;
                }
            }

            _bestIndividual = bestIndividual;
        }

        private Individual GetRandomIndividual()
        {
            var min = _optimizationFunction.MinX;
            var max = _optimizationFunction.MaxX;
            var interval = Math.Abs(max - min);

            var randomCoordinates = Enumerable.Range(0, Dimensions)
                .Select(e => _random.NextDouble() * interval - max);

            return new Individual(randomCoordinates);
        }
    }
}
