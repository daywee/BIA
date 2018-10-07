using System;

namespace Shared.TestFunctions
{
    public class BoothFunction : FunctionBase
    {
        public override string Name { get; } = "Booth";
        public override double MinX { get; } = -10;
        public override double MaxX { get; } = 10;

        public override double Calculate(params double[] x)
        {
            if (x.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(x), "Booth's function is only defined on a 2D space.");

            return Math.Pow(x[0] + 2 * x[1] - 7, 2) + Math.Pow(2 * x[0] + x[1] - 5, 2);
        }
    }
}
