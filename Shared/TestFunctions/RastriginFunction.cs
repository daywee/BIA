using System;
using System.Linq;

namespace Shared.TestFunctions
{
    public class RastriginFunction : FunctionBase
    {
        public override string Name { get; } = "Rastrigin";
        public override double MinX { get; } = -5.12;
        public override double MaxX { get; } = 5.12;
        public override double Calculate(params double[] x)
        {
            return 10 * x.Length * x.Sum(xi => xi * xi - 10 * Math.Cos(2 * Math.PI * xi));
        }
    }
}
