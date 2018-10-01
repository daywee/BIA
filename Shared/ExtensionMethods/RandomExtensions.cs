using System;
using System.Threading;

namespace Shared.ExtensionMethods
{
    public static class RandomExtensions
    {
        // Box-Muller Transformation (http://mathworld.wolfram.com/Box-MullerTransformation.html)
        public static (double, double) NextNormalDistribution2D(this Random source, double sigma = 1, double mu = 0)
        {
            double random1 = source.NextDouble();
            double random2 = source.NextDouble();

            double z1 = Math.Sqrt(-2 * Math.Log(random1)) * Math.Cos(2 * Math.PI * random2);
            double z2 = Math.Sqrt(-2 * Math.Log(random1)) * Math.Sin(2 * Math.PI * random2);

            double x1 = sigma * z1 + mu;
            double x2 = sigma * z2 + mu;

            return (x1, x2);
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
