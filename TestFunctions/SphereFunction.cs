using System.Linq;

namespace TestFunctions
{
    public class SphereFunction : FunctionBase
    {
        public override double Calculate(params double[] x)
        {
            return x.Sum(e => e * e);
        }
    }
}
