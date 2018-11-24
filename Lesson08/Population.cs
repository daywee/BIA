using System.Collections.Generic;
using System.Linq;

namespace Lesson08
{
    public class Population
    {
        public int Generation { get; set; }
        public List<CitiesSequence> CurrentPopulation { get; set; }
        public CitiesSequence BestSequence { get; private set; }
        public IAlgorithm Algorithm { get; }
        private readonly int _populationSize;

        public CitiesSequence BaseCitiesSequence { get; }

        public Population(CitiesSequence baseCitiesSequence, IAlgorithm algorithm, int populationSize = 50)
        {
            BaseCitiesSequence = baseCitiesSequence;
            Algorithm = algorithm;
            _populationSize = populationSize;
            CreateNewPopulation();
        }

        private void GeneratePopulation()
        {
            CurrentPopulation = Algorithm.GeneratePopulation(this);
        }

        public void Evolve()
        {
            GeneratePopulation();
            SetBestSequence();
            Generation++;
        }

        public void CreateNewPopulation()
        {
            CurrentPopulation = Algorithm.SeedPopulation(BaseCitiesSequence, _populationSize);
            CurrentPopulation.ForEach(e => e.CalculateCost());
            SetBestSequence();
            Generation = 0;
        }

        private void SetBestSequence()
        {
            BestSequence = CurrentPopulation
                .OrderBy(e => e.Cost)
                .First();
        }
    }
}
