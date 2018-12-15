using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson10.OptimizationAlgorithms
{
    public class Nsga2Algorithm : IAlgorithm
    {
        public int MaxPopulation { get; } = 10;

        private readonly Random _random = new Random();

        public List<Individual> SeedPopulation(Population population)
        {
            return new[] { -2, -1, 0, 2, 4, 1 }
                .Select(e =>
                {
                    var x = new Individual(e);
                    x.CalculateCost(population.OptimizationFunction1, population.OptimizationFunction2);
                    return x;
                })
                .ToList();

            //return Enumerable.Range(0, MaxPopulation)
            //    .Select(_ => CreateRandomIndividual(population.OptimizationFunction1, population.OptimizationFunction2, population.Dimension))
            //    .ToList();
        }

        public List<Individual> GeneratePopulation(Population population)
        {
            var children = new List<Individual>();
            foreach (var individual in population.CurrentPopulation)
            {
                var toChoose = population.CurrentPopulation.Except(new[] { individual }).ToList();
                var randomIndividual = toChoose[_random.Next(toChoose.Count)];

                Individual c1, c2;
                do
                {
                    (c1, c2) = individual.Crossover(randomIndividual, _random);

                } while (double.IsNaN(c1.Position) || double.IsNaN(c2.Position));

                c1.Mutate(_random);
                c2.Mutate(_random);

                c1.ApplyBounds(population.OptimizationFunction1, population.OptimizationFunction2, _random);
                c2.ApplyBounds(population.OptimizationFunction1, population.OptimizationFunction2, _random);

                c1.CalculateCost(population.OptimizationFunction1, population.OptimizationFunction2);

                children.Add(c1);
                children.Add(c2);
            }

            var fronts = FastNondominatedSort(children.Concat(population.CurrentPopulation).ToList());

            return fronts
                .SelectMany(e => e)
                .Take(MaxPopulation)
                .ToList();
        }

        private List<Individual>[] FastNondominatedSort(List<Individual> population)
        {
            // init fronts
            var F = Enumerable.Range(0, population.Count)
                .Select(_ => new List<Individual>())
                .ToArray();

            foreach (var p in population)
            {
                foreach (var q in population)
                {
                    if (p.DoesDominate(q))
                        p.S.Add(q);
                    else if (q.DoesDominate(p))
                        p.N++;
                }
                if (p.N == 0)
                    F[0].Add(p);
                p.S = p.S.Except(new[] { p }).ToList();
            }

            int i = 0;
            while (F[i].Count != 0)
            {
                var H = new List<Individual>();

                foreach (var p in F[i])
                {
                    foreach (var q in p.S)
                    {
                        q.N--;
                        if (q.N == 0)
                        {
                            H.Add(q);
                            q.Rank = i + 1;
                        }
                    }
                }

                i++;
                F[i] = H;
            }

            // reset all values
            foreach (var p in population)
            {
                p.N = 0;
                p.Rank = 0;
                p.S = new List<Individual>();
            }

            return F;
        }

        private Individual CreateRandomIndividual(OneDimensionFunctionBase optimizationFunction1, OneDimensionFunctionBase optimizationFunction2, int dimension)
        {
            var min = optimizationFunction1.MinX;
            var max = optimizationFunction2.MaxX;
            var interval = Math.Abs(max - min);

            var randomCoordinates = _random.NextDouble() * interval - max;

            var newIndividual = new Individual(randomCoordinates);
            newIndividual.CalculateCost(optimizationFunction1, optimizationFunction2);

            return newIndividual;
        }
    }
}
