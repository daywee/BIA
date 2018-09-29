namespace Shared.TestFunctions
{
    public abstract class FunctionBase
    {
        public abstract string Name { get; }
        public abstract double Calculate(params double[] x);
    }
}
