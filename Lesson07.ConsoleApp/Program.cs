using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson07.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int iterations = 100;
            int evolutions = 1000;
            int populationSize = 50;

            var population = new Population(CitiesSequence.GetDefaultSequence(), new GeneticAlgorithm(), populationSize);
            var results = new List<double>();

            for (int iteration = 0; iteration < iterations; iteration++)
            {
                population.CreateNewPopulation();

                for (int evolution = 0; evolution < evolutions; evolution++)
                {
                    population.Evolve();
                }

                results.Add(population.BestSequence.Cost);

                Console.WriteLine();
                Console.WriteLine($"Iteration: {iteration}");
                Console.WriteLine($"Distance: {population.BestSequence.Cost}");
            }

            Console.WriteLine();
            Console.WriteLine($"Best distance: {results.Min()}");
        }
    }
}
