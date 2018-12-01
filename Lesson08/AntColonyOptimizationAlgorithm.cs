using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lesson08
{
    public class AntColonyOptimizationAlgorithm : IAlgorithm
    {
        public int PopulationSize { get; set; }

        public List<City> Cities { get; set; }

        private const double Alpha = 3; // original 0.2
        private const double Beta = 2; // original 0.6
        private const double Rho = 0.01; // original 0.6
        private const double Q = 2;

        private readonly Matrix _pheromoneMatrix;
        private readonly Matrix _distanceMatrix;
        private readonly Random _random = new Random();

        private const bool PresentationTsp = false;

        public AntColonyOptimizationAlgorithm(IEnumerable<City> cities)
        {
            if (PresentationTsp)
            {
                Cities = new List<City>
                {
                    new City(10, 0, "A", 0),
                    new City(100, 0, "B", 1),
                    new City(40, 70, "C", 2),
                    new City(0, 100, "D", 3),
                    new City(90, 100, "E", 4),
                };
                _pheromoneMatrix = new Matrix(Cities.Count, 0.2);
                _distanceMatrix = new Matrix(Cities.Count);
                InitDistanceMatrix(Cities);
            }
            else
            {
                Cities = cities.ToList();
                _pheromoneMatrix = new Matrix(Cities.Count, 0.2);
                _distanceMatrix = new Matrix(Cities.Count);
                InitDistanceMatrix(Cities);
            }
        }

        public List<CitiesSequence> SeedPopulation(CitiesSequence baseSequence, int populationSize)
        {
            return new List<CitiesSequence> { new CitiesSequence { Cities = Cities } };
        }

        public List<CitiesSequence> GeneratePopulation(Population population)
        {
            var antTours = new List<CitiesSequence>();

            foreach (var city in Cities)
            {
                var currentCity = city;
                var antTour = new CitiesSequence();
                antTour.Cities.Add(currentCity);
                var otherCities = Cities.Except(new[] { currentCity }).ToList();

                while (otherCities.Count > 0)
                {
                    var chosenCity = ChooseCityToGo(currentCity, otherCities);
                    antTour.Cities.Add(chosenCity);
                    otherCities.Remove(chosenCity);
                    currentCity = chosenCity;
                }
                // todo: make last step (thats probably not necessary)

                antTour.CalculateCost(_distanceMatrix);
                antTours.Add(antTour);
            }

            UpdatePheromoneTrail(antTours);
            antTours.Add(population.BestSequence);
            return antTours;
        }

        private void UpdatePheromoneTrail(List<CitiesSequence> tours)
        {
            // pheromone trail evaporation
            _pheromoneMatrix.MultiplyBy(1 - Rho);

            foreach (var tour in tours)
            {
                double tao = 1 / tour.Cost;
                for (int i = 0; i < tour.Cities.Count - 1; i++)
                {
                    var c1 = tour.Cities[i];
                    var c2 = tour.Cities[i + 1];
                    _pheromoneMatrix[c1.Id, c2.Id] += tao;
                }

                {
                    var c1 = tour.Cities[0];
                    var c2 = tour.Cities.Last();
                    _pheromoneMatrix[c1.Id, c2.Id] += tao;
                }
            }
        }

        private City ChooseCityToGo(City fromCity, List<City> citiesToGo)
        {
            int ChooseIntervalIndex(double randomNumber, double[] intervals)
            {
                for (int i = 0; i < intervals.Length; i++)
                    if (randomNumber <= intervals[i])
                        return i;

                throw new InvalidOperationException();
            }

            var probabilisticValues = citiesToGo
                .Select(cityToGo => CalculateProbabilisticValue(fromCity, cityToGo, citiesToGo))
                .ToList();

            // todo: might be good idea to set last interval to 1
            var probabilisticValueIntervals = Enumerable.Range(0, probabilisticValues.Count)
                .Select(i => probabilisticValues.Take(i + 1).Sum())
                .ToArray();

            double r = _random.NextDouble();
            var chosenIndex = ChooseIntervalIndex(r, probabilisticValueIntervals);

            return citiesToGo[chosenIndex];
        }

        private double CalculateProbabilisticValue(City fromCity, City toCity, List<City> allCities)
        {
            double ValueBetweenTwoCities(City a, City b)
            {
                double pheromone = _pheromoneMatrix[a.Id, b.Id];
                double distance = _distanceMatrix[a.Id, b.Id];

                var result = Math.Pow(pheromone, Alpha) * Math.Pow(Q / distance, Beta);
                if (double.IsInfinity(result))
                    Debugger.Break();

                return result;
            }

            double upper = ValueBetweenTwoCities(fromCity, toCity);
            double lower = allCities
                .Select(city => ValueBetweenTwoCities(fromCity, city))
                .Sum();

            return upper / lower;
        }

        private void InitDistanceMatrix(List<City> cities)
        {
            if (PresentationTsp)
            {
                int A = 0;
                int B = 1;
                int C = 2;
                int D = 3;
                int E = 4;

                _distanceMatrix[A, B] = 100;
                _distanceMatrix[A, D] = 50;
                _distanceMatrix[A, E] = 70;
                _distanceMatrix[A, C] = 60;
                _distanceMatrix[B, E] = 60;
                _distanceMatrix[B, C] = 60;
                _distanceMatrix[B, D] = 70;
                _distanceMatrix[C, D] = 90;
                _distanceMatrix[C, E] = 40;
                _distanceMatrix[D, E] = 150;
            }
            else
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
}
