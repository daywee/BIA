﻿using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
{
    public class HillClimbingAlgorithm : IAlgorithm<Individual>
    {
        private readonly Random _random = new Random();

        public int MaxPopulation { get; } = 50;
        public int SeedingPopulationCount { get; } = 1;

        public List<Individual> SeedPopulation(Population<Individual> population)
        {
            return Enumerable.Range(0, SeedingPopulationCount)
                .Select(_ => population.GetRandomIndividual())
                .ToList();
        }

        public List<Individual> GeneratePopulation(Population<Individual> population)
        {
            return Enumerable.Range(0, population.MaxPopulationCount)
                .Select(_ =>
                {
                    var x = new Vector(_random.NextNormalDistribution(population.Dimensions, population.StandardDeviation, population.Mean));
                    x += population.BestIndividual.Position; // translate by current best individual

                    return new Individual(x, population.OptimizationFunction.Calculate(x.ToArray()));
                })
                .ToList();
        }
    }
}