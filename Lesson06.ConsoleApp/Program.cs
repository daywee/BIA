using Lesson06.OptimizationAlgorithms;
using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson06.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 30;
            const int dimension = 2;
            const int evolutions = 100;

            var deRandResults = new List<Individual>();
            var deCurrentToBestResults = new List<Individual>();

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"Iteration: {i}");

                var builder = PopulationBuilder.GetBuilder()
                    .WithOptimizationFunction<SchwefelFunction>()
                    .WithOptimizationTarget(OptimizationTarget.Minimum)
                    .WithDimension(dimension);

                var deRand = builder
                    .WithAlgorithm<DifferentialEvolutionRand>()
                    .Build();
                var deCurrentToBest = builder
                    .WithAlgorithm<DifferentialEvolutionCurrentToBest>()
                    .Build();

                deRand.CreateNewPopulation();
                deCurrentToBest.CreateNewPopulation();

                for (int evolution = 0; evolution < evolutions; evolution++)
                {
                    deRand.Evolve();
                    deCurrentToBest.Evolve();
                }

                Console.WriteLine($"DE/rand/1: {deRand.BestIndividual.Cost}");
                Console.WriteLine($"DE/current-to-best/1: {deCurrentToBest.BestIndividual.Cost}");

                deRandResults.Add(deRand.BestIndividual);
                deCurrentToBestResults.Add(deCurrentToBest.BestIndividual);
            }

            Console.WriteLine();
            Console.WriteLine($"Dimesnions {dimension}");
            Console.WriteLine($"DE/rand/1 after {iterations}x iterations: {deRandResults.Average(e => e.Cost)} (best: {deRandResults.Min(e => e.Cost)})");
            Console.WriteLine($"DE/current-to-best/1 {iterations}x iterations: {deCurrentToBestResults.Average(e => e.Cost)} (best: {deCurrentToBestResults.Min(e => e.Cost)})");
        }
    }
}
