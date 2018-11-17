using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson07
{
    public class Population
    {
        public int Generation { get; set; }
        public List<CitiesSequence> CurrentPopulation { get; set; }
        public CitiesSequence BestSequence { get; protected set; }
        public IAlgorithm Algorithm { get; }
        private int _populationSize;

        private readonly Random _random = new Random();
        public CitiesSequence BaseCitiesSequence { get; }

        public Population(CitiesSequence baseCitiesSequence, IAlgorithm algorithm, int populationSize = 500)
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
