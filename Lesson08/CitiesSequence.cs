using System.Collections.Generic;
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
                    //new City(180, 60, "N", 13),
                    //new City(20, 40, "O", 14),
                    //new City(100, 40, "P", 15),
                    //new City(200, 40, "Q", 16),
                    //new City(20, 20, "R", 17),
                    //new City(60, 20, "S", 18),
                    //new City(160, 20, "T", 19)
                }
            };
        }
    }
}