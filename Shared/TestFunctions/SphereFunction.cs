using System.Linq;

namespace Shared.TestFunctions
{
    public class SphereFunction : FunctionBase
    {
        public override string Name { get; } = "Sphere";

        public override double Calculate(params double[] x)
        {
            return x.Sum(e => e * e);
        }
    }
}
