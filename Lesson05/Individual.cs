using System.Collections.Generic;

namespace Lesson05
{
    public class Individual
    {
        public Vector Position;
        public double Cost { get; set; }

        public Individual()
        {
            
        }

        public Individual(int dimensions)
        {
            Position = new Vector(dimensions);
        }

        public Individual(IEnumerable<double> coordinates)
        {
            Position = new Vector(coordinates);
        }

        public Individual(Vector coordinates)
        {
            Position = coordinates;
        }

        public Individual(IEnumerable<double> coordinates, double cost)
            : this(coordinates)
        {
            Cost = cost;
        }

        public Individual(Vector coordinates, double cost)
            : this(coordinates)
        {
            Cost = cost;
        }
    }
}
