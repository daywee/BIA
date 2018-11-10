using Shared.TestFunctions;
using System;

namespace Lesson05
{
    public class PopulationBuilder
    {
        private Type _algorithmType;
        private FunctionBase _function;
        private int _dimension;
        private OptimizationTarget _optimizationTarget;

        private PopulationBuilder()
        {
        }

        public static PopulationBuilder GetBuilder()
        {
            return new PopulationBuilder();
        }

        public PopulationBuilder WithAlgorithm<TAlgorithm>() where TAlgorithm : IAlgorithm
        {
            _algorithmType = typeof(TAlgorithm);
            return this;
        }

        public PopulationBuilder WithOptimizationFunction<TOptimizationFunction>() where TOptimizationFunction : FunctionBase, new()
        {
            _function = new TOptimizationFunction();
            return this;
        }

        public PopulationBuilder WithDimension(int dimension)
        {
            _dimension = dimension;
            return this;
        }

        public PopulationBuilder WithOptimizationTarget(OptimizationTarget optimizationTarget)
        {
            _optimizationTarget = optimizationTarget;
            return this;
        }

        public Population Build()
        {
            if (_algorithmType == typeof(ParticleSwarmAlgorithm))
                return new Population<ParticleSwarmIndividual>(_function, new ParticleSwarmAlgorithm(_function.MinX, _function.MaxX), _dimension, _optimizationTarget);
            if (_algorithmType == typeof(HillClimbingAlgorithm))
                return new Population<Individual>(_function, new HillClimbingAlgorithm(), _dimension, _optimizationTarget);
            if (_algorithmType == typeof(SimulatedAnnealingAlgorithm))
                return new Population<Individual>(_function, new SimulatedAnnealingAlgorithm(), _dimension, _optimizationTarget);
            if (_algorithmType == typeof(SomaAlgorithm))
                return new Population<Individual>(_function, new SomaAlgorithm(), _dimension, _optimizationTarget);

            throw new InvalidOperationException();
        }
    }
}
