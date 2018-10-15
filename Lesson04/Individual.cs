using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson04
{
    public class Individual
    {
        public int Dimensions { get; }
        public double Cost { get; set; }

        private readonly double[] _x;

        public Individual(int dimensions)
        {
            Dimensions = dimensions;
            _x = new double[dimensions];
        }

        public Individual(IEnumerable<double> coordinates)
        {
            _x = coordinates.ToArray();
            Dimensions = _x.Length;
        }

        public Individual(IEnumerable<double> coordinates, double cost)
            : this(coordinates)
        {
            Cost = cost;
        }

        public double this[int index]
        {
            get
            {
                if (index < 0 || index >= Dimensions)
                    throw new IndexOutOfRangeException("Index cannot be lesser then zero or greater then dimension");

                return _x[index];
            }
            set
            {
                if (index < 0 || index >= Dimensions)
                    throw new IndexOutOfRangeException("Index cannot be lesser then zero or greater then dimension");

                _x[index] = value;
            }
        }

        #region Operators
        public static Individual operator +(Individual a, Individual b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] + b[i];

            return new Individual(newCoordinates);
        }

        public static Individual operator -(Individual a, Individual b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] - b[i];

            return new Individual(newCoordinates);
        }

        public static Individual operator *(Individual x, double alpha)
        {
            var newCoordinates = new double[x.Dimensions];
            for (int i = 0; i < x.Dimensions; i++)
                newCoordinates[i] = x[i] * alpha;

            return new Individual(newCoordinates);
        }

        public static Individual operator *(double alpha, Individual x)
        {
            return x * alpha;
        }

        public static Individual operator *(Individual a, Individual b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] * b[i];

            return new Individual(newCoordinates);
        }
        #endregion

        public double[] ToArray() => _x.ToArray();
    }
}
