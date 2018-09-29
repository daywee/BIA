using System;
using System.Threading;

namespace Shared.ExtensionMethods
{
    public static class RandomExtensions
    {
        // Box-Muller Transformation (http://mathworld.wolfram.com/Box-MullerTransformation.html)
        public static (double, double) NextNormalDistribution2D(this Random source)
        {
            double x1 = source.NextDouble();
            double x2 = source.NextDouble();

            double z1 = Math.Sqrt(-2 * Math.Log(x1)) * Math.Cos(2 * Math.PI * x2);
            double z2 = Math.Sqrt(-2 * Math.Log(x1)) * Math.Sin(2 * Math.PI * x2);

            return (z1, z2);
        }

        // https://blogs.sas.com/content/iml/2016/03/30/generate-uniform-2d-ball.html
        public static (double, double) NextUniformDistribution2D(this Random source)
        {
            var r = Math.Sqrt(source.NextDouble());
            var theta = source.NextDouble() * 2 * Math.PI;

            var x = r * Math.Cos(theta);
            var y = r * Math.Sin(theta);

            return (x, y);
        }
    }
}
