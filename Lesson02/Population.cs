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
        public FunctionBase OptimizationFunction { get; }
        public Individual BestIndividual { get; private set; }
        public double StandardDeviation { get; set; } = 1; // sigma
        public double Mean { get; set; } = 0; // mu

        private readonly Random _random = new Random();

        public Population(FunctionBase optimizationFunction, int maxPopulationCount, int dimensions, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
        {
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

        public Individual CalculateMean()
        {
            var mean = new Individual(Dimensions);

            foreach (var individual in CurrentPopulation)
                for (int i = 0; i < Dimensions; i++)
                    mean[i] += individual[i];

            for (int i = 0; i < Dimensions; i++)
                mean[i] /= CurrentPopulation.Count;

            mean.Result = OptimizationFunction.Calculate(mean.ToArray());

            return mean;
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
                    var x = _random.NextNormalDistribution(Dimensions, StandardDeviation, Mean);
                    Enumerable.Range(0, Dimensions)
                        .ForEach(dimension => x[dimension] += BestIndividual[dimension]); // translate by current best individual

                    return new Individual(x, OptimizationFunction.Calculate(x));
                })
                .ToList();
        }

        private void SetBestIndividual()
        {
            var bestIndividual = CurrentPopulation.First();
            for (int i = 1; i < MaxPopulationCount; i++)
            {
                var currentIndividual = CurrentPopulation[i];

                if ((OptimizationTarget == OptimizationTarget.Maximum && currentIndividual.Result > bestIndividual.Result)
                    || (OptimizationTarget == OptimizationTarget.Minimum && currentIndividual.Result < bestIndividual.Result))
                {
                    bestIndividual = currentIndividual;
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
                .Select(e => _random.NextDouble() * interval - max)
                .ToArray();

            return new Individual(randomCoordinates, OptimizationFunction.Calculate(randomCoordinates));
        }
    }

    public enum OptimizationTarget
    {
        Minimum,
        Maximum
    }
}
