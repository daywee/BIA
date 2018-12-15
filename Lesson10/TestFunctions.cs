using System;

namespace Lesson10
{
    public abstract class OneDimensionFunctionBase
    {
        public abstract string Name { get; }
        public abstract int MinX { get; }
        public abstract int MaxX { get; }

        public abstract double Calculate(double x);
    }

    public class FirstTestFunction : OneDimensionFunctionBase
    {
        public override string Name { get; } = "-x^2";
        public override int MinX { get; } = -55;
        public override int MaxX { get; } = 55;

        public override double Calculate(double x)
        {
            return -Math.Pow(x, 2);
        }
    }

    public class SecondTestFunction : OneDimensionFunctionBase
    {
        public override string Name { get; } = "-(x-2)^2";
        public override int MinX { get; } = -55;
        public override int MaxX { get; } = 55;

        public override double Calculate(double x)
        {
            return -Math.Pow(x - 2, 2);
        }
    }
}
