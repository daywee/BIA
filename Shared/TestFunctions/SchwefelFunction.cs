using System;
using System.Linq;

namespace Shared.TestFunctions
{
    public class SchwefelFunction : FunctionBase
    {
        public override string Name { get; } = "Schwefel";

        public override double Calculate(params double[] x)
        {
            return 418.9829 * x.Length - x.Sum(e => e * Math.Sin(Math.Sqrt(Math.Abs(e))));
        }
    }
}
