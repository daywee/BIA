using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson04
{
    public class Population
    {
        public int Dimensions { get; }
        public OptimizationTarget OptimizationTarget { get; }
        public int MaxPopulationCount { get; }
        public List<Individual> CurrentPopulation { get; private set; }
        public int Generation { get; private set; }
        public FunctionBase OptimizationFunction { get; }
        public IAlgorithm Algorithm { get; }
        public Individual BestIndividual { get; private set; }
        public double StandardDeviation { get; set; } = 1; // sigma
        public double Mean { get; set; } = 0; // mu

        private readonly Random _random = new Random();

        public Population(FunctionBase optimizationFunction, IAlgorithm algorithm, int dimensions, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
        {
            OptimizationFunction = optimizationFunction;
            Algorithm = algorithm;
            MaxPopulationCount = algorithm.MaxPopulation;
            Dimensions = dimensions;
            OptimizationTarget = optimizationTarget;
            CreateNewPopulation();
        }

        public void CreateNewPopulation()
        {
            CurrentPopulation = Enumerable.Range(0, Algorithm.SeedingPopulationCount)
                .Select(_ => GetRandomIndividual())
                .ToList();

            if (OptimizationTarget == OptimizationTarget.Minimum)
                BestIndividual = CurrentPopulation.OrderBy(e => e.Cost).First();
            else
                BestIndividual = CurrentPopulation.OrderByDescending(e => e.Cost).First();

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

            mean.Cost = OptimizationFunction.Calculate(mean.ToArray());

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
            CurrentPopulation = Algorithm.GeneratePopulation(this);
        }

        private void SetBestIndividual()
        {
            var bestIndividual = CurrentPopulation.First();
            for (int i = 1; i < MaxPopulationCount; i++)
            {
                var currentIndividual = CurrentPopulation[i];

                if ((OptimizationTarget == OptimizationTarget.Maximum && currentIndividual.Cost > bestIndividual.Cost)
                    || (OptimizationTarget == OptimizationTarget.Minimum && currentIndividual.Cost < bestIndividual.Cost))
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
