using Lesson09.OptimizationAlgorithms;
using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson09
{
    public abstract class Population
    {
        public int Dimensions { get; }
        public FunctionBase OptimizationFunction { get; }
        public int Generation { get; protected set; }
        public List<Individual> CurrentPopulation { get; protected set; }
        public Individual BestIndividual { get; protected set; }
        public double StandardDeviation { get; set; } = 1; // sigma
        public double Mean { get; set; } = 0; // mu

        protected Population(FunctionBase optimizationFunction, int dimensions)
        {
            OptimizationFunction = optimizationFunction;
            Dimensions = dimensions;
        }

        public Individual CalculateMean()
        {
            var mean = new Individual(Dimensions);

            foreach (var individual in CurrentPopulation)
                for (int i = 0; i < Dimensions; i++)
                    mean.Position[i] += individual.Position[i];

            for (int i = 0; i < Dimensions; i++)
                mean.Position[i] /= CurrentPopulation.Count;

            mean.CalculateCost(OptimizationFunction);

            return mean;
        }

        public abstract void Evolve();
        public abstract void CreateNewPopulation();
    }

    // todo: try to remove new() constraint
    public class Population<TIndividual> : Population where TIndividual : Individual, new()
    {
        public OptimizationTarget OptimizationTarget { get; }

        public new List<TIndividual> CurrentPopulation
        {
            get => base.CurrentPopulation.Cast<TIndividual>().ToList();
            private set => base.CurrentPopulation = value.Cast<Individual>().ToList();
        }

        public IAlgorithm<TIndividual> Algorithm { get; }

        private readonly Random _random = new Random();

        public Population(FunctionBase optimizationFunction, IAlgorithm<TIndividual> algorithm, int dimensions, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
            : base(optimizationFunction, dimensions)
        {
            Algorithm = algorithm;
            OptimizationTarget = optimizationTarget;
            CreateNewPopulation();
        }

        public override void CreateNewPopulation()
        {
            CurrentPopulation = Algorithm.SeedPopulation(this);

            if (OptimizationTarget == OptimizationTarget.Minimum)
                BestIndividual = CurrentPopulation.OrderBy(e => e.Cost).First();
            else
                BestIndividual = CurrentPopulation.OrderByDescending(e => e.Cost).First();

            Generation = 0;
        }

        public override void Evolve()
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
            for (int i = 1; i < Algorithm.MaxPopulation; i++)
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

        // todo: maybe it should be part of a algorithm
        internal TIndividual GetRandomIndividual()
        {
            var min = OptimizationFunction.MinX;
            var max = OptimizationFunction.MaxX;
            var interval = Math.Abs(max - min);

            var randomCoordinates = Enumerable.Range(0, Dimensions)
                .Select(e => _random.NextDouble() * interval - max)
                .ToArray();

            var newIndividual = new TIndividual
            {
                Position = new Vector(randomCoordinates),
                Cost = OptimizationFunction.Calculate(randomCoordinates)
            };

            return newIndividual;
        }
    }

    public enum OptimizationTarget
    {
        Minimum,
        Maximum
    }
}
