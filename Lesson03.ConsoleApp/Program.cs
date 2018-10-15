using Shared.TestFunctions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            const int iterations = 30;
            const int dimensions = 10;
            const int evolutions = 2000;

            var hcResults = new List<Individual>();
            var saResults = new List<Individual>();

            for (int i = 0; i < iterations; i++)
            {
                Console.WriteLine($"Iteration: {i}");
                var optimizationFunction = new SchwefelFunction();
                var hillClimbing = new Population(optimizationFunction, new HillClimbingAlgorithm(), dimensions);
                var simulatedAnnealing = new Population(optimizationFunction, new SimulatedAnnealingAlgorithm(), dimensions);

                for (int evolution = 0; evolution < evolutions / hillClimbing.MaxPopulationCount; evolution++)
                    hillClimbing.Evolve();
                for (int evolution = 0; evolution < evolutions; evolution++)
                    simulatedAnnealing.Evolve();

                Console.WriteLine($"Hill climbing: {hillClimbing.BestIndividual.Cost}");
                Console.WriteLine($"Simulated annealing: {simulatedAnnealing.BestIndividual.Cost}");

                hcResults.Add(hillClimbing.BestIndividual);
                saResults.Add(simulatedAnnealing.BestIndividual);
            }

            Console.WriteLine();
            Console.WriteLine($"Dimesnions {dimensions}");
            Console.WriteLine($"Hill climbing after {iterations}x iterations: {hcResults.Sum(e => e.Cost) / iterations} (best: {hcResults.Min(e => e.Cost)})");
            Console.WriteLine($"Simulated annealing {iterations}x iterations: {saResults.Sum(e => e.Cost) / iterations} (best: {saResults.Min(e => e.Cost)})");
        }
    }
}
