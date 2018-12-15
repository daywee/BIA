using System.Collections.Generic;

namespace Lesson10.OptimizationAlgorithms
{
    public interface IAlgorithm
    {
        int MaxPopulation { get; }
        List<Individual> GeneratePopulation(Population population);
        List<Individual> SeedPopulation(Population population);
    }
}
