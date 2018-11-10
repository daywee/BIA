using System.Collections.Generic;

namespace Lesson05
{
    public interface IAlgorithm
    {
    }

    public interface IAlgorithm<TIndividual> : IAlgorithm where TIndividual: Individual, new()
    {
        int MaxPopulation { get; }
        int SeedingPopulationCount { get; }
        List<TIndividual> GeneratePopulation(Population<TIndividual> population);
        List<TIndividual> SeedPopulation(Population<TIndividual> population);
    }
}
