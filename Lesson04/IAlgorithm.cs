using System.Collections.Generic;

namespace Lesson04
{
    public interface IAlgorithm
    {
        int MaxPopulation { get; }
        int SeedingPopulationCount { get; }
        List<Individual> GeneratePopulation(Population population);
    }
}
