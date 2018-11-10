using Shared.TestFunctions;
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

        public void CalculateCost(FunctionBase function)
        {
            Cost = function.Calculate(Position.ToArray());
        }
    }
}
