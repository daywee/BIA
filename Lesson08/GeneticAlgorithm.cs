using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson08
{
    public class GeneticAlgorithm : IAlgorithm
    {
        public int PopulationSize { get; set; }

        private readonly Random _random = new Random();

        public List<CitiesSequence> SeedPopulation(CitiesSequence baseSequence, int populationSize)
        {
            PopulationSize = populationSize;

            return Enumerable.Range(0, populationSize)
                .Select(_ => GenerateRandomSequence(baseSequence))
                .ToList();
        }

        public List<CitiesSequence> GeneratePopulation(Population population)
        {
            var result = new List<CitiesSequence>();
            foreach (var currentSequence in population.CurrentPopulation)
            {
                var allExceptCurrent = population.CurrentPopulation.Except(new[] { currentSequence }).ToList();
                var randomSequence = allExceptCurrent[_random.Next(allExceptCurrent.Count)];

                var breeded = CrossBreed(currentSequence, randomSequence);
                RandomMutation(breeded);

                if (breeded.Cost <= currentSequence.Cost)
                    result.Add(breeded);
                else
                    result.Add(currentSequence);
            }

            return result;
        }

        private CitiesSequence CrossBreed(CitiesSequence s1, CitiesSequence s2)
        {
            int[] bounds = new int[2];

            bounds[0] = bounds[1] = _random.Next(s1.Cities.Count); // probably should be -1
            while (bounds[0] == bounds[1])
                bounds[1] = _random.Next(s1.Cities.Count);
            bounds = bounds.OrderBy(e => e).ToArray();

            var availableCities = s1.Cities.ToList();
            var usedCities = new List<City>();

            var newSequence = new City[s1.Cities.Count];
            for (int i = 0; i < s1.Cities.Count; i++)
            {
                // todo: check >= bound
                if (i < bounds[0] || i >= bounds[1])
                {
                    newSequence[i] = s1.Cities[i];
                    availableCities.Remove(s1.Cities[i]);
                    usedCities.Add(s1.Cities[i]);
                }
            }

            // fill unused cities between bounds
            for (int i = bounds[0]; i < bounds[1]; i++)
            {
                var tryCity = s2.Cities[i];
                if (usedCities.Contains(tryCity))
                {
                    newSequence[i] = availableCities.First();
                    availableCities.RemoveAt(0);
                    usedCities.Add(newSequence[i]);
                }
                else
                {
                    newSequence[i] = tryCity;
                    availableCities.Remove(tryCity);
                    usedCities.Add(tryCity);
                }
            }

            var result = new CitiesSequence();
            result.Cities.AddRange(newSequence);
            result.CalculateCost();

            return result;
        }

        private void RandomMutation(CitiesSequence sequence)
        {
            // todo: make constant
            if (_random.NextDouble() > 0.05)
            {
                int i1, i2;
                i1 = i2 = _random.Next(sequence.Cities.Count);

                while (i1 == i2)
                {
                    i2 = _random.Next(sequence.Cities.Count);
                }

                var temp = sequence.Cities[i1];
                sequence.Cities[i1] = sequence.Cities[i2];
                sequence.Cities[i2] = temp;
            }

            sequence.CalculateCost();
        }

        private CitiesSequence GenerateRandomSequence(CitiesSequence baseSequence)
        {
            var availableCities = baseSequence.Cities.ToList();
            var newSequence = new CitiesSequence();

            while (availableCities.Count > 0)
            {
                var city = availableCities[_random.Next(availableCities.Count)];
                availableCities.Remove(city);
                newSequence.Cities.Add(city);
            }

            return newSequence;
        }
    }
}
