using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson08
{
    public class AntColonyOptimizationAlgorithm : IAlgorithm
    {
        public int PopulationSize { get; set; }

        public List<City> Cities { get; set; }

        private const double Alpha = 0.2;
        private const double Beta = 0.6;
        private const double Ro = 0.6;

        private readonly Matrix _pheromoneMatrix;
        private readonly Matrix _distanceMatrix;
        private readonly Random _random = new Random();

        public AntColonyOptimizationAlgorithm(IEnumerable<City> cities)
        {
            Cities = cities.ToList();
            _pheromoneMatrix = new Matrix(Cities.Count, 0.2);
            _distanceMatrix = new Matrix(Cities.Count);
            InitDistanceMatrix(Cities);
        }

        public List<CitiesSequence> SeedPopulation(CitiesSequence baseSequence, int populationSize)
        {
            return new List<CitiesSequence> { new CitiesSequence { Cities = Cities } };
        }

        public List<CitiesSequence> GeneratePopulation(Population population)
        {
            var antTours = new List<List<City>>();

            foreach (var city in Cities)
            {
                var currentCity = city;
                var antTour = new List<City> { currentCity }; // todo: use cities sequence
                var otherCities = Cities.Except(new[] { city }).ToList();

                while (otherCities.Count > 0)
                {
                    var chosenCity = ChooseCityToGo(currentCity, otherCities);
                    antTour.Add(chosenCity);
                    otherCities.Remove(chosenCity);
                    currentCity = chosenCity;
                }
                // todo: make last step

                antTours.Add(antTour);
            }

            UpdatePheromoneTrail(antTours);
        }

        private void UpdatePheromoneTrail(List<List<City>> tours)
        {
            foreach (var tour in tours)
            {
                for (int i = 0; i < tour.Count - 1; i++)
                {
                    double tao = _pheromoneMatrix[i, i + 1];

                }
            }



            var notCalculated = new List<City>(cities);

            foreach (var c1 in cities)
            {
                notCalculated.Remove(c1);
                foreach (var c2 in notCalculated)
                {
                    double distance = c1.Position.EuclideanDistanceTo(c2.Position);
                    _distanceMatrix[c1.Id, c2.Id] = distance;
                }
            }
        }

        private City ChooseCityToGo(City fromCity, List<City> citiesToGo)
        {
            var probabilisticValues = citiesToGo
                .Select(cityToGo => CalculateProbabilisticValue(fromCity, cityToGo, citiesToGo))
                .ToList();

            var intervals = Enumerable.Range(0, probabilisticValues.Count)
                .Select(i => probabilisticValues.Take(i + 1).Sum())
                .ToList();

            // todo: check if sum always equals to 1
            double r = _random.NextDouble();
            int chosenIntervalIndex = intervals.FindLastIndex(interval => interval <= r);

            return citiesToGo[chosenIntervalIndex];
        }

        private double CalculateProbabilisticValue(City fromCity, City toCity, List<City> allCities)
        {
            double ValueBetweenTwoCities(City a, City b)
            {
                double pheromone = _pheromoneMatrix[a.Id, b.Id];
                double distance = _distanceMatrix[a.Id, b.Id];

                return Math.Pow(pheromone, Alpha) * Math.Pow(1 / distance, Beta);
            }

            double upper = ValueBetweenTwoCities(fromCity, toCity);
            double lower = allCities
                .Select(city => ValueBetweenTwoCities(fromCity, city))
                .Sum();

            return upper / lower;
        }

        private void InitDistanceMatrix(List<City> cities)
        {
            var notCalculated = new List<City>(cities);

            foreach (var c1 in cities)
            {
                notCalculated.Remove(c1);
                foreach (var c2 in notCalculated)
                {
                    double distance = c1.Position.EuclideanDistanceTo(c2.Position);
                    _distanceMatrix[c1.Id, c2.Id] = distance;
                }
            }
        }
    }
}
