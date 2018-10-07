using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03
{
    public class Individual
    {
        public int Dimensions { get; }
        public double Result { get; set; }

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

        public Individual(IEnumerable<double> coordinates, double result)
            : this(coordinates)
        {
            Result = result;
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

        public double[] ToArray() => _x.ToArray();
    }
}
