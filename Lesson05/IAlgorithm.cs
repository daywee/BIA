using System.Collections.Generic;

namespace Lesson05
{
    public interface IAlgorithm
    {
        int MaxPopulation { get; }
        int SeedingPopulationCount { get; }
        List<Individual> GeneratePopulation(Population population);
    }
}
