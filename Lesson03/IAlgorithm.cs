using System.Collections.Generic;

namespace Lesson03
{
    public interface IAlgorithm
    {
        int MaxPopulation { get; }
        List<Individual> GeneratePopulation(Population population);
    }
}
