namespace Shared.TestFunctions
{
    public abstract class FunctionBase
    {
        public abstract string Name { get; }
        // usually evaluated on the hypercube in interval from MinX to MaxX
        public abstract double MinX { get; }
        public abstract double MaxX { get; }
        public abstract double Calculate(params double[] x);
    }
}
