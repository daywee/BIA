using System.Collections.Generic;

namespace Lesson07
{
    public interface IAlgorithm
    {
        int PopulationSize { get; set; }
        List<CitiesSequence> SeedPopulation(CitiesSequence baseSequence, int populationSize);
        List<CitiesSequence> GeneratePopulation(Population population);
    }
}
