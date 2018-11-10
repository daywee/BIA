using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
{
    // mel by taky obsahovat setrvacnost?
    public class ParticleSwarmAlgorithm : IAlgorithm<ParticleSwarmIndividual>
    {
        public int MaxPopulation { get; } = 10;
        public int SeedingPopulationCount { get; } = 10;
        public double MaxVelocity { get; set; }

        private readonly Random _random = new Random();

        public ParticleSwarmAlgorithm(double functionMinX, double functionMaxX)
        {
            MaxVelocity = Math.Abs(functionMaxX - functionMinX) / 20;
        }

        public List<ParticleSwarmIndividual> SeedPopulation(Population<ParticleSwarmIndividual> population)
        {
            return Enumerable.Range(0, SeedingPopulationCount)
                .Select(_ =>
                {
                    var individual = population.GetRandomIndividual();

                    var randomVelocity = Enumerable.Range(0, population.Dimensions)
                        .Select(e => _random.NextDouble() * MaxVelocity)
                        .ToArray();

                    individual.Velocity = new Vector(randomVelocity);
                    return individual;
                })
                .ToList();
        }

        public List<ParticleSwarmIndividual> GeneratePopulation(Population<ParticleSwarmIndividual> population)
        {
            throw new NotImplementedException();
        }
    }
}
