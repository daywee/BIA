using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
{
    public class Vector
    {
        public int Dimensions { get; }

        private readonly double[] _x;

        public Vector(int dimensions)
        {
            Dimensions = dimensions;
            _x = new double[dimensions];
        }

        public Vector(IEnumerable<double> coordinates)
        {
            _x = coordinates.ToArray();
            Dimensions = _x.Length;
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

        public override string ToString()
        {
            var positions = _x.Select((e, i) => $"x{i}: {e:0.000}");
            return string.Join(", ", positions);
        }

        #region Operators
        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] + b[i];

            return new Vector(newCoordinates);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] - b[i];

            return new Vector(newCoordinates);
        }

        public static Vector operator *(Vector x, double alpha)
        {
            var newCoordinates = new double[x.Dimensions];
            for (int i = 0; i < x.Dimensions; i++)
                newCoordinates[i] = x[i] * alpha;

            return new Vector(newCoordinates);
        }

        public static Vector operator *(double alpha, Vector x)
        {
            return x * alpha;
        }

        public static Vector operator +(Vector x, double alpha)
        {
            var newCoordinates = new double[x.Dimensions];
            for (int i = 0; i < x.Dimensions; i++)
                newCoordinates[i] = x[i] + alpha;

            return new Vector(newCoordinates);
        }

        public static Vector operator +(double alpha, Vector x)
        {
            return x + alpha;
        }

        public static Vector operator *(Vector a, Vector b)
        {
            if (a.Dimensions != b.Dimensions)
                throw new InvalidOperationException("Individuals must have same dimension");

            var newCoordinates = new double[a.Dimensions];
            for (int i = 0; i < a.Dimensions; i++)
                newCoordinates[i] = a[i] * b[i];

            return new Vector(newCoordinates);
        }
        #endregion

        public double[] ToArray() => _x.ToArray();
    }
}
