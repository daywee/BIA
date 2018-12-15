using System;

namespace Lesson10
{
    public class Individual
    {
        public double Position;
        public double Cost1 { get; set; }
        public double Cost2 { get; set; }

        public const float MutationConstant = 0.8f;

        private string BinaryPosition
        {
            get
            {
                long x = BitConverter.DoubleToInt64Bits(Position);
                return Convert.ToString(x, 2);
            }
            set
            {
                long x = Convert.ToInt64(value);
                Position = BitConverter.Int64BitsToDouble(x);
            }
        }

        public Individual()
        {
        }

        public Individual(double position)
        {
            Position = position;
        }

        public void CalculateCost(OneDimensionFunctionBase f1, OneDimensionFunctionBase f2)
        {
            Cost1 = f1.Calculate(Position);
            Cost2 = f2.Calculate(Position);
        }

        public bool DoesDominate(Individual other, OptimizationTarget optimizationTarget = OptimizationTarget.Minimum)
        {
            if (optimizationTarget == OptimizationTarget.Minimum)
            {
                return Cost1 < other.Cost1 && Cost2 < other.Cost2;
            }

            if (optimizationTarget == OptimizationTarget.Maximum)
            {
                return Cost1 > other.Cost1 && Cost2 > other.Cost2;
            }

            throw new ArgumentException("Optimization target is not valid");
        }

        public void Mutate(Random random)
        {
            if (random.NextDouble() < MutationConstant)
            {
                int index = random.Next(BinaryPosition.Length - 1);
                char b = BinaryPosition[index] == '0' ? '1' : '0';
                var arr = BinaryPosition.ToCharArray();
                arr[index] = b;
                BinaryPosition = arr.ToString();
            }
        }

        public (Individual, Individual) Crossover(Individual other, Random random)
        {
            int index = random.Next(BinaryPosition.Length - 1);
            var i1 = BinaryPosition.Substring(0, index);
            var i2 = BinaryPosition.Substring(index, BinaryPosition.Length);

            var o1 = other.BinaryPosition.Substring(0, index);
            var o2 = other.BinaryPosition.Substring(index + 1, other.BinaryPosition.Length);

            var n1 = new Individual { BinaryPosition = i1 + o2 };
            var n2 = new Individual { BinaryPosition = o1 + i2 };

            return (n1, n2);
        }

        public void ApplyBounds(OneDimensionFunctionBase optimizationFunction, Random random)
        {
            if (Position < optimizationFunction.MinX || Position > optimizationFunction.MaxX)
            {
                Position = random.Next(optimizationFunction.MinX, optimizationFunction.MaxX);
            }
        }
    }
}
