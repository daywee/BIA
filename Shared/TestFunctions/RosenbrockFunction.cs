using System;

namespace Shared.TestFunctions
{
    public class RosenbrockFunction : FunctionBase
    {
        public override string Name { get; } = "Rosenbrock";
        public override double MinX { get; } = -5;
        public override double MaxX { get; } = 10;

        public override double Calculate(params double[] x)
        {
            double sum = 0;
            for (int i = 0; i < x.Length - 1; i++)
            {
                double leftPart = 100 * Math.Pow(x[i + 1] - x[i] * x[i], 2);
                double rightPart = Math.Pow(1 - x[i], 2);
                sum += leftPart + rightPart;
            }

            return sum;
        }
    }
}
