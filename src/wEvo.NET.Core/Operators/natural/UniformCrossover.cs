﻿/*
 * wevo.NET - Distributed Evolutionary Computation Library
 *
 * Based on wevo and wevo2 libraries.
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor,
   Boston, MA  02110-1301  USA
 */

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