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
        public OptimizationTarget OptimizationTarget { get; }
        public int MaxPopulationCount { get; }
        public List<Individual> CurrentPopulation { get; private set; }
        public int Generation { get; private set; }

        public FunctionBase OptimizationFunction { get; set; }
        private readonly Random _random = new Random();
        public Individual BestIndividual { get; private set; }

        public Population(FunctionBase optimizationFunction, int maxPopulationCount, int dimensions, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
        {
            if (dimensions != 2)
                throw new ArgumentOutOfRangeException(nameof(dimensions), "Only 2 dimensions are currently supported");

            OptimizationFunction = optimizationFunction;
            MaxPopulationCount = maxPopulationCount;
            Dimensions = dimensions;
            OptimizationTarget = optimizationTarget;
            CreateNewPopulation();
        }

        public void CreateNewPopulation()
        {
            BestIndividual = GetRandomIndividual();
            CurrentPopulation = new List<Individual> { BestIndividual };
            Generation = 0;
        }

        public void Evolve()
        {
            GeneratePopulation();
            SetBestIndividual();
            Generation++;
        }

        private void GeneratePopulation()
        {
            CurrentPopulation = Enumerable.Range(0, MaxPopulationCount)
                .Select(_ =>
                {
                    var (x1, x2) = _random.NextNormalDistribution2D();

                    // translate by current best individual
                    x1 += BestIndividual[0];
                    x2 += BestIndividual[1];

                    return new Individual(Dimensions) { [0] = x1, [1] = x2 };
                })
                .ToList();
        }

        private void SetBestIndividual()
        {
            Individual bestIndividual = CurrentPopulation.First();
            double bestValue = OptimizationFunction.Calculate(bestIndividual[0], bestIndividual[1]);

            for (int i = 1; i < MaxPopulationCount; i++)
            {
                Individual currentIndividual = CurrentPopulation[i];
                double currentValue = OptimizationFunction.Calculate(currentIndividual[0], currentIndividual[1]);

                if ((OptimizationTarget == OptimizationTarget.Maximum && currentValue > bestValue)
                    || (OptimizationTarget == OptimizationTarget.Minimum && currentValue < bestValue))
                {
                    bestIndividual = currentIndividual;
                    bestValue = currentValue;
                }
            }

            BestIndividual = bestIndividual;
        }

        private Individual GetRandomIndividual()
        {
            var min = OptimizationFunction.MinX;
            var max = OptimizationFunction.MaxX;
            var interval = Math.Abs(max - min);

            var randomCoordinates = Enumerable.Range(0, Dimensions)
                .Select(e => _random.NextDouble() * interval - max);

            return new Individual(randomCoordinates);
        }
    }

    public enum OptimizationTarget
    {
        Minimum,
        Maximum
    }
}
