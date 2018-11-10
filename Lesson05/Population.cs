using Shared.ExtensionMethods;
using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using Lesson05.OptimizationAlgorithms;

namespace Lesson05
{
    public abstract class Population
    {
        public int Dimensions { get; }
        public FunctionBase OptimizationFunction { get; }
        public int Generation { get; protected set; }
        public List<Individual> CurrentPopulation { get; protected set; }
        public List<Individual> AdditionalIndividualsToRender { get; set; }
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

            mean.Cost = OptimizationFunction.Calculate(mean.Position.ToArray());

            return mean;
        }

        public abstract void Evolve();
        public abstract void CreateNewPopulation();
    }

    // todo: try to remove new() constraint
    public class Population<TIndividual> : Population where TIndividual : Individual, new()
    {
        public OptimizationTarget OptimizationTarget { get; }
        public int MaxPopulationCount { get; }

        public new List<TIndividual> CurrentPopulation
        {
            get => base.CurrentPopulation.Cast<TIndividual>().ToList();
            private set => base.CurrentPopulation = value.Cast<Individual>().ToList();
        }

        public new List<TIndividual> AdditionalIndividualsToRender
        {
            get => base.AdditionalIndividualsToRender.Cast<TIndividual>().ToList();
            set => base.AdditionalIndividualsToRender = value.Cast<Individual>().ToList();
        }
        public IAlgorithm<TIndividual> Algorithm { get; }

        private readonly Random _random = new Random();

        public Population(FunctionBase optimizationFunction, IAlgorithm<TIndividual> algorithm, int dimensions, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
            : base(optimizationFunction, dimensions)
        {
            AdditionalIndividualsToRender = new List<TIndividual>();
            Algorithm = algorithm;
            MaxPopulationCount = algorithm.MaxPopulation;
            OptimizationTarget = optimizationTarget;
            CreateNewPopulation();
        }

        public override void CreateNewPopulation()
        {
            CurrentPopulation = Algorithm.SeedPopulation(this);

            ApplyBounds(CurrentPopulation);

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
            ApplyBounds(CurrentPopulation);
            ApplyBounds(AdditionalIndividualsToRender);
        }

        // todo: maybe it should be part of a algorithm
        internal void ApplyBounds(IEnumerable<Individual> population)
        {
            population.ForEach(ApplyBounds);
        }

        internal void ApplyBounds(Individual individual)
        {
            double GetRandomCoordinate()
            {
                double min = OptimizationFunction.MinX;
                double max = OptimizationFunction.MaxX;
                double interval = Math.Abs(max - min);

                return _random.NextDouble() * interval - max;
            }

            for (int i = 0; i < Dimensions; i++)
            {
                if (individual.Position[i] < OptimizationFunction.MinX)
                {
                    individual.Position[i] = GetRandomCoordinate();
                    individual.CalculateCost(OptimizationFunction);
                }
                else if (individual.Position[i] > OptimizationFunction.MaxX)
                {
                    individual.Position[i] = GetRandomCoordinate();
                    individual.CalculateCost(OptimizationFunction);
                }
            }
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
