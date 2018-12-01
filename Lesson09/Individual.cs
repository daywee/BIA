using Shared.TestFunctions;
using System;
using System.Collections.Generic;

namespace Lesson09
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

        // random parameter is used to improve 'randomness'
        // if each Individual would have it's own Random then NextDouble() results
        // would be same if Individuals are initialized at the same time
        public void ApplyBounds(FunctionBase optimizationFunction, Random random)
        {
            double GetRandomCoordinate()
            {
                double min = optimizationFunction.MinX;
                double max = optimizationFunction.MaxX;
                double interval = Math.Abs(max - min);

                return random.NextDouble() * interval - max;
            }

            for (int i = 0; i < Position.Dimensions; i++)
            {
                if (Position[i] < optimizationFunction.MinX)
                    Position[i] = GetRandomCoordinate();
                else if (Position[i] > optimizationFunction.MaxX)
                    Position[i] = GetRandomCoordinate();
            }
        }
    }
}
