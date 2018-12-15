using Lesson10.OptimizationAlgorithms;
using Shared.TestFunctions;
using System.Collections.Generic;
using System.Linq;

namespace Lesson10
{
    public class Population
    {
        public int Dimension { get; }
        public OneDimensionFunctionBase OptimizationFunction1 { get; }
        public OneDimensionFunctionBase OptimizationFunction2 { get; }
        public int Generation { get; protected set; }
        public List<Individual> CurrentPopulation { get; protected set; }
        public Individual BestIndividual { get; protected set; }
        public OptimizationTarget OptimizationTarget { get; }
        public IAlgorithm Algorithm { get; }

        public Population(OneDimensionFunctionBase optimizationFunction1, OneDimensionFunctionBase optimizationFunction2, IAlgorithm algorithm,
            int dimension, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
        {
            OptimizationFunction1 = optimizationFunction1;
            OptimizationFunction2 = optimizationFunction2;
            Dimension = dimension;
            Algorithm = algorithm;
            OptimizationTarget = optimizationTarget;

            CreateNewPopulation();
        }

        //public Individual CalculateMean()
        //{
        //    var mean = new Individual(Dimension);

        //    foreach (var individual in CurrentPopulation)
        //        for (int i = 0; i < Dimension; i++)
        //            mean.Position[i] += individual.Position[i];

        //    for (int i = 0; i < Dimension; i++)
        //        mean.Position[i] /= CurrentPopulation.Count;

        //    mean.CalculateCost(OptimizationFunction1, OptimizationFunction2);

        //    return mean;
        //}

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
            for (int i = 1; i < Algorithm.MaxPopulation; i++)
            {
                var currentIndividual = CurrentPopulation[i];

                if ((OptimizationTarget == OptimizationTarget.Maximum && currentIndividual.Cost1 > bestIndividual.Cost1)
                    || (OptimizationTarget == OptimizationTarget.Minimum && currentIndividual.Cost1 < bestIndividual.Cost1))
                {
                    bestIndividual = currentIndividual;
                }
            }

            BestIndividual = bestIndividual;
        }

        public void CreateNewPopulation()
        {
            CurrentPopulation = Algorithm.SeedPopulation(this);

            if (OptimizationTarget == OptimizationTarget.Minimum)
                BestIndividual = CurrentPopulation.OrderBy(e => e.Cost1).First();
            else
                BestIndividual = CurrentPopulation.OrderByDescending(e => e.Cost1).First();

            Generation = 0;
        }
    }

    public enum OptimizationTarget
    {
        Minimum,
        Maximum
    }
}
