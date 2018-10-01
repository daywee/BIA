using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson02
{
    public class Individual
    {
        public int Dimensions { get; }
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
    }
}
