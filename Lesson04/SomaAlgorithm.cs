﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Lesson04
{
    public class SomaAlgorithm : IAlgorithm
    {
        public double PathLength { get; }
        public double StepSize { get; }
        public double Prt { get; }
        public int MaxPopulation { get; } = 10;
        public int SeedingPopulationCount { get; } = 10;

        private readonly Random _random = new Random();

        public SomaAlgorithm(double pathLength = 3, double stepSize = 0.11, double prt = 0.1, int maxPopulation = 10)
        {
            PathLength = pathLength;
            StepSize = stepSize;
            Prt = prt;
            MaxPopulation = maxPopulation;
        }

        public List<Individual> GeneratePopulation(Population population)
        {
            population.AdditionalIndividualsToRender.Clear();
            var leader = population.BestIndividual;
            var others = population.CurrentPopulation.Except(new[] { leader });
            var newPopulation = new List<Individual>();

            foreach (var other in others)
            {
                var individualSteps = new List<Individual>();
                for (double t = 0; t < PathLength; t += StepSize)
                {
                    var moveVector = leader - other;
                    var individualStep = other + moveVector * t * new Individual(GeneratePrtVector(population.Dimensions));
                    individualStep.Cost = population.OptimizationFunction.Calculate(individualStep.ToArray());
                    individualSteps.Add(individualStep);
                }

                List<Individual> orderedIndividualSteps;
                if (population.OptimizationTarget == OptimizationTarget.Minimum)
                    orderedIndividualSteps = individualSteps.OrderBy(e => e.Cost).ToList();
                else
                    orderedIndividualSteps = individualSteps.OrderByDescending(e => e.Cost).ToList();
                newPopulation.Add(orderedIndividualSteps.First());
                population.AdditionalIndividualsToRender.AddRange(orderedIndividualSteps.Skip(1));
            }

            newPopulation.Add(leader);

            return newPopulation;
        }

        private double[] GeneratePrtVector(int dimensions)
        {
            return Enumerable.Range(0, dimensions)
                .Select(i => _random.NextDouble() < Prt ? (double)1 : 0)
                .ToArray();
        }
    }
}
