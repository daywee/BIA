using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson09.OptimizationAlgorithms
{
    public class DifferentialEvolutionCurrentToBest : IAlgorithm<Individual>
    {
        public int MaxPopulation { get; } = 10;

        private readonly double _mutationConstant; // F
        private readonly double _crossover; // CR
        private readonly Random _random = new Random();

        public DifferentialEvolutionCurrentToBest(double mutationConstant = 0.5, double crossover = 0.9)
        {
            _mutationConstant = mutationConstant;
            _crossover = crossover;
        }

        public List<Individual> SeedPopulation(Population<Individual> population)
        {
            return Enumerable.Range(0, MaxPopulation)
                .Select(_ => population.GetRandomIndividual())
                .ToList();
        }

        public List<Individual> GeneratePopulation(Population<Individual> population)
        {
            var newPopulation = new List<Individual>();

            var fiveBest = population.CurrentPopulation
                .OrderBy(e => e.Cost)
                .Take(5)
                .ToList();

            foreach (var individual in population.CurrentPopulation)
            {
                var (v1, v2) = GetRandomIndividualPositions(population.CurrentPopulation, individual);
                var randomBest = fiveBest[_random.Next(4)];
                var noiseVector = GetNoiseVector(individual.Position, randomBest.Position, v1, v2);

                var trialIndividual = GetTrialIndividual(individual, noiseVector, population.Dimensions);
                trialIndividual.ApplyBounds(population.OptimizationFunction, _random);
                trialIndividual.CalculateCost(population.OptimizationFunction);

                if (trialIndividual.Cost <= individual.Cost)
                    newPopulation.Add(trialIndividual);
                else
                    newPopulation.Add(individual);
            }

            return newPopulation;
        }

        private (Vector, Vector) GetRandomIndividualPositions(IEnumerable<Individual> individuals, Individual exceptIndividual)
        {
            var remaining = individuals.Except(new[] { exceptIndividual }).ToList();
            var chosenVectors = new List<Vector>();

            for (int i = 0; i < 2; i++)
            {
                var chosen = remaining[_random.Next(remaining.Count)];
                remaining.Remove(chosen);
                chosenVectors.Add(chosen.Position);
            }

            return (chosenVectors[0], chosenVectors[1]);
        }

        private Vector GetNoiseVector(Vector current, Vector best, Vector v1, Vector v2)
        {
            return current + _mutationConstant * (best - current) + _mutationConstant * (v1 - v2);
        }

        private Individual GetTrialIndividual(Individual individual, Vector noiseVector, int dimension)
        {
            var trialVector = new Vector(noiseVector.ToArray());
            for (int i = 0; i < dimension; i++)
            {
                if (_random.NextDouble() >= _crossover)
                {
                    trialVector[i] = individual.Position[i];
                }
            }

            return new Individual(trialVector);
        }
    }
}
