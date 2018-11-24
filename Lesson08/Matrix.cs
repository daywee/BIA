using System;

namespace Lesson08
{
    public class Matrix
    {
        public int Dimension { get; set; }

        private readonly double[,] _table;

        public Matrix(int dimension, double? initialValue = null)
        {
            Dimension = dimension;
            _table = new double[dimension, dimension];

            if (initialValue.HasValue)
                for (int i = 0; i < dimension; i++)
                    for (int j = 0; j < dimension; j++)
                        _table[i, j] = initialValue.Value;
        }

        public double this[int i, int j]
        {
            get
            {
                if (i > Dimension || j > Dimension)
                    throw new IndexOutOfRangeException();

                return _table[i, j];
            }
            set
            {
                if (i > Dimension || j > Dimension)
                    throw new IndexOutOfRangeException();

                _table[i, j] = value;
                _table[j, i] = value;
            }
        }
    }
}
