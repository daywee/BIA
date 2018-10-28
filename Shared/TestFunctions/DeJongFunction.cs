using System;

namespace Shared.TestFunctions
{
    public class DeJongFunction : FunctionBase
    {
        public override string Name { get; } = "De Jong";
        public override double MinX { get; } = -65.536;
        public override double MaxX { get; } = 65.536;

        private static readonly int[,] A;

        static DeJongFunction()
        {
            A = GetMatrixA();
        }

        public override double Calculate(params double[] x)
        {
            if (x.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(x), "De Jong's function is only defined on a 2D space.");

            double sum = 0;
            for (int i = 0; i < 25; i++)
            {
                double belowFration = (i + 1) + Math.Pow(i + x[0] - A[0, i], 6) + Math.Pow(x[1] - A[1, i], 6);
                sum += 1 / belowFration;
            }

            return Math.Pow(0.002 + sum, -1);
        }

        private static int[,] GetMatrixA()
        {
            var A = new int[2, 25];
            var a = new[] { -32, -16, 0, 16, 32 };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    A[0, i * 5 + j] = a[j];
                    A[1, i * 5 + j] = a[i];
                }
            }

            return A;
        }
    }
}
