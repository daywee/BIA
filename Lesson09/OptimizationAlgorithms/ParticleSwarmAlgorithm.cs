using Shared.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson09.OptimizationAlgorithms
{
    // mel by taky obsahovat setrvacnost?
    public class ParticleSwarmAlgorithm : IAlgorithm<ParticleSwarmIndividual>
    {
        public int MaxPopulation { get; } = 10;
        public double MaxVelocity { get; set; }
        public double C1 { get; } // learning factor
        public double C2 { get; } // learning factor

        private readonly Random _random = new Random();

        public ParticleSwarmAlgorithm(double functionMinX, double functionMaxX, double c1 = 2, double c2 = 2)
        {
            MaxVelocity = Math.Abs(functionMaxX - functionMinX) / 20;
            C1 = c1;
            C2 = c2;
        }

        public List<ParticleSwarmIndividual> SeedPopulation(Population<ParticleSwarmIndividual> population)
        {
            return Enumerable.Range(0, MaxPopulation)
                .Select(_ =>
                {
                    var individual = population.GetRandomIndividual();

                    var randomVelocity = Enumerable.Range(0, population.Dimensions)
                        .Select(e => _random.NextDoubleWithNegative() * MaxVelocity)
                        .ToArray();

                    individual.Velocity = new Vector(randomVelocity);
                    individual.PersonalBestPosition = individual.Position;
                    individual.PersonalBestCost = individual.Cost;
                    return individual;
                })
                .ToList();
        }

        // todo: improve swarm to converge
        // I think the problem is when bounds are applied, some individuals are randomly placed and make swarm to move randomly too
        // solution might be to change learning factors
        public List<ParticleSwarmIndividual> GeneratePopulation(Population<ParticleSwarmIndividual> population)
        {
            var newPopulation = new List<ParticleSwarmIndividual>();
            var globalBest = population.BestIndividual.Position;
            foreach (var individual in population.CurrentPopulation)
            {
                var newIndividual = new ParticleSwarmIndividual();
                var v1 = C1 * _random.NextDouble() * (individual.PersonalBestPosition - individual.Position);
                var v2 = C2 * _random.NextDouble() * (globalBest - individual.Position);
                newIndividual.Velocity = individual.Velocity + v1 + v2;

                // generate new v if v > vmax
                var newIndividualVelocity = newIndividual.Velocity.ToArray();
                for (int i = 0; i < newIndividualVelocity.Length; i++)
                {
                    if (newIndividualVelocity[i] > MaxVelocity)
                    {
                        newIndividualVelocity[i] = _random.NextDoubleWithNegative() * MaxVelocity;
                    }
                }
                newIndividual.Velocity = new Vector(newIndividualVelocity);

                newIndividual.Position = individual.Position + newIndividual.Velocity;
                newIndividual.ApplyBounds(population.OptimizationFunction, _random);
                newIndividual.CalculateCost(population.OptimizationFunction);

                if (newIndividual.Cost > individual.PersonalBestCost)
                {
                    newIndividual.PersonalBestPosition = newIndividual.Position;
                    newIndividual.PersonalBestCost = newIndividual.Cost;
                }
                else
                {
                    newIndividual.PersonalBestPosition = individual.Position;
                    newIndividual.PersonalBestCost = individual.PersonalBestCost;
                }

                newPopulation.Add(newIndividual);
            }

            return newPopulation;
        }
    }
}
