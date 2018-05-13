using System.Collections.Generic;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Natural
{
    /**
    * Performs a crossover of two natural number individuals by
    * creating two offsprings that contain random subset of genes
    * from two parents.
    */
    public class UniformCrossover : Operator<NaturalVector>
    {
        /** 0.5. */
        private static double DEFAULT_PROBABILITY = 0.5;

        /** RNG. */
        private wevoRandom random;

        /**
         * Creates uniform crossover operator.
         * @param random Random number generator.
         */
        public UniformCrossover(wevoRandom random)
        {
            this.random = random;
        }

        /**
         * Creates uniform crossover operator.
         */
        public UniformCrossover() : this(new dotNetRandom())
        {
        }

        /** {@inheritDoc}. */
        public Population<NaturalVector> Apply(Population<NaturalVector> population)
        {
            List<NaturalVector> result = new List<NaturalVector>();
            List<NaturalVector> individuals = population.GetIndividuals();
            for (int i = 0; i < individuals.Count / 2; i++)
            {
                long[] p1 = individuals[2 * i].GetValues();
                long[] p2 = individuals[2 * i + 1].GetValues();
                long[] o1 = new long[p1.Length];
                long[] o2 = new long[p1.Length];

                for (int j = 0; j < p1.Length; j++)
                {
                    if (random.NextDouble(0, 1) < DEFAULT_PROBABILITY)
                    {
                        o1[j] = p1[j];
                        o2[j] = p2[j];
                    }
                    else
                    {
                        o1[j] = p2[j];
                        o2[j] = p1[j];
                    }
                }
                result.Add(new NaturalVector(o1));
                result.Add(new NaturalVector(o2));
            }

            if (result.Count < individuals.Count)
            {
                result.Add(individuals[result.Count - 1]);
            }

            return new Population<NaturalVector>(result);
        }
    }
}
