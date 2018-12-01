using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson08
{
    public class CitiesSequence
    {
        public List<City> Cities { get; set; } = new List<City>();
        public double Cost { get; set; }


        public void CalculateCost()
        {
            double cost = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
                cost += Cities[i].Position.EuclideanDistanceTo(Cities[i + 1].Position);
            cost += Cities[0].Position.EuclideanDistanceTo(Cities.Last().Position);

            Cost = cost;
        }

        public void CalculateCost(Matrix distanceMatrix)
        {
            double cost = 0;
            for (int i = 0; i < Cities.Count - 1; i++)
                cost += distanceMatrix[Cities[i].Id, Cities[i + 1].Id];
            cost += distanceMatrix[Cities[0].Id, Cities.Last().Id];

            Cost = cost;
        }

        public static CitiesSequence GetDefaultSequence()
        {
            return new CitiesSequence
            {
                Cities = new List<City>
                {
                    new City(60, 200, "A", 0),
                    new City(80, 200, "B", 1),
                    new City(80, 180, "C", 2),
                    new City(140, 180, "D", 3),
                    new City(20, 160, "E", 4),
                    new City(100, 160, "F", 5),
                    new City(200, 160, "G", 6),
                    new City(140, 140, "H", 7),
                    new City(40, 120, "I", 8),
                    new City(100, 120, "J", 9),
                    new City(180, 100, "K", 10),
                    new City(60, 80, "L", 11),
                    new City(120, 80, "M", 12),
                    new City(180, 60, "N", 13),
                    new City(20, 40, "O", 14),
                    new City(100, 40, "P", 15),
                    new City(200, 40, "Q", 16),
                    new City(20, 20, "R", 17),
                    new City(60, 20, "S", 18),
                    new City(160, 20, "T", 19)
                }
            };
        }

        public static CitiesSequence GetAtt48()
        {
            var cities = File.ReadAllLines("../../../Datasets/TSP/att48.tsp")
                .Skip(6)
                .Take(48)
                .Select(line => line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                .Select(values => new { X = int.Parse(values[1]) / 10, Y = int.Parse(values[2]) / 10 })
                .Select((value, i) => new City(value.X, value.Y, i.ToString(), i))
                .ToList();

            return new CitiesSequence
            {
                Cities = cities
            };
        }

        public static CitiesSequence GetAli535()
        {
            var cities = File.ReadAllLines("../../../Datasets/TSP/ali535.tsp")
                .Skip(7)
                .Take(535)
                .Select(line => line.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Replace('.',',')).ToArray())
                .Select((values, i) => new City(double.Parse(values[1]) * 10, double.Parse(values[2]) * 10, i.ToString(), i))
                .ToList();

            return new CitiesSequence
            {
                Cities = cities
            };
        }
    }
}