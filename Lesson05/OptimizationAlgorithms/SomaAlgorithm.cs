﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05.OptimizationAlgorithms
{
    public class SomaAlgorithm : IAlgorithm<Individual>
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

        public List<Individual> SeedPopulation(Population<Individual> population)
        {
            return Enumerable.Range(0, SeedingPopulationCount)
                .Select(_ => population.GetRandomIndividual())
                .ToList();
        }

        // todo: rendering of additional individuals does not work
        public List<Individual> GeneratePopulation(Population<Individual> population)
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
                    var moveVector = leader.Position - other.Position;
                    var individualStepVector = other.Position + moveVector * t * GeneratePrtVector(population.Dimensions);
                    var individualStep = new Individual(individualStepVector);
                    individualStep.CalculateCost(population.OptimizationFunction);
                    population.ApplyBounds(individualStep);
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

        private Vector GeneratePrtVector(int dimensions)
        {
            var vector = Enumerable.Range(0, dimensions)
                .Select(i => _random.NextDouble() < Prt ? (double)1 : 0);

            return new Vector(vector);
        }
    }
}
