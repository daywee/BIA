using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Lesson10
{
    [DebuggerDisplay("Position={Position}, c1={Cost1}, c2={Cost2}")]
    public class Individual
    {
        public double Position;
        private double _cost1;
        private double _cost2;

        public double Cost1
        {
            get => _cost1;
            set
            {
                if (double.IsNaN(_cost1))
                    Debugger.Break();
                _cost1 = value;
            }
        }

        public double Cost2
        {
            get => _cost2;
            set
            {
                if (double.IsNaN(_cost1))
                    Debugger.Break();
                _cost2 = value;
            }

        }

        public const float MutationConstant = 0.8f;

        public List<Individual> S { get; set; } = new List<Individual>();
        public int N { get; set; }
        public int Rank { get; set; }

        private string BinaryPosition
        {
            get
            {
                long x = BitConverter.DoubleToInt64Bits(Position);
                return Convert.ToString(x, 2).PadLeft(64, '0');
            }
            set
            {
                long x = Convert.ToInt64(value, 2);
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

        public bool DoesDominate(Individual other, OptimizationTarget optimizationTarget = OptimizationTarget.Maximum)
        {
            if (optimizationTarget == OptimizationTarget.Minimum)
            {
                return Cost1 <= other.Cost1 && Cost2 <= other.Cost2;
            }

            if (optimizationTarget == OptimizationTarget.Maximum)
            {
                return Cost1 >= other.Cost1 && Cost2 >= other.Cost2;
            }

            throw new ArgumentException("Optimization target is not valid");
        }

        public void Mutate(Random random)
        {
            if (random.NextDouble() < MutationConstant)
            {
                var original = Position;

                int index = random.Next(BinaryPosition.Length - 1);
                char b = BinaryPosition[index] == '0' ? '1' : '0';
                var arr = BinaryPosition.ToCharArray();
                arr[index] = b;
                BinaryPosition = string.Join(string.Empty, arr);

                if (double.IsNaN(Position))
                    Position = original;
            }
        }

        public (Individual, Individual) Crossover(Individual other, Random random)
        {
            if (BinaryPosition.Length != other.BinaryPosition.Length)
                throw new InvalidOperationException("Length must be the same");

            int index = random.Next(BinaryPosition.Length - 1);
            var i1 = BinaryPosition.Substring(0, index);
            var i2 = BinaryPosition.Substring(index);

            var o1 = other.BinaryPosition.Substring(0, index);
            var o2 = other.BinaryPosition.Substring(index);

            var n1 = new Individual { BinaryPosition = i1 + o2 };
            var n2 = new Individual { BinaryPosition = o1 + i2 };

            return (n1, n2);
        }

        public void ApplyBounds(OneDimensionFunctionBase optimizationFunction1, OneDimensionFunctionBase optimizationFunction2, Random random)
        {
            if (Position < optimizationFunction1.MinX || Position > optimizationFunction1.MaxX)
            {
                Position = random.Next(optimizationFunction1.MinX, optimizationFunction1.MaxX);
            }

            if (Position < optimizationFunction2.MinX || Position > optimizationFunction2.MaxX)
            {
                Position = random.Next(optimizationFunction2.MinX, optimizationFunction2.MaxX);
            }
        }
    }
}
