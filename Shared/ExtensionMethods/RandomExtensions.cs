﻿using System;

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

        public static double[] NextNormalDistribution(this Random source, int dimensions, double sigma = 1, double mu = 0)
        {
            var result = new double[dimensions];
            for (int i = 0; i < dimensions / 2; i++)
            {
                var (x1, x2) = source.NextNormalDistribution2D(sigma, mu);
                result[i * 2] = x1;
                result[i * 2 + 1] = x2;
            }

            if (dimensions % 2 == 1)
                result[dimensions - 1] = source.NextNormalDistribution2D(sigma, mu).Item1;

            return result;
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

        public static double NextDoubleWithNegative(this Random random)
        {
            return random.NextDouble() * 2 - 1;
        }
    }
}
