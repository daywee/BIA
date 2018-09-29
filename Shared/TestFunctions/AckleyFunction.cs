using System;
using System.Linq;

namespace Shared.TestFunctions
{
    public class AckleyFunction : FunctionBase
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly FunctionBase SphereFunction = new SphereFunction();

        public AckleyFunction()
            : this(20, 0.2, 2 * Math.PI)
        {
        }

        public AckleyFunction(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public override string Name { get; } = "Ackley";

        public override double Calculate(params double[] x)
        {
            double leftParenthesis = -_b * Math.Sqrt(1 / (double)x.Length * SphereFunction.Calculate(x));
            double rightParenthesis = 1 / (double)x.Length * x.Select(e => Math.Cos(_c * e)).Sum();

            return -_a * Math.Exp(leftParenthesis) - Math.Exp(rightParenthesis) + _a + Math.Exp(1);
        }
    }
}
