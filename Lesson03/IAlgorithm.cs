using System.Collections.Generic;

namespace Lesson03
{
    public interface IAlgorithm
    {
        List<Individual> GeneratePopulation(Population population);
    }
}
