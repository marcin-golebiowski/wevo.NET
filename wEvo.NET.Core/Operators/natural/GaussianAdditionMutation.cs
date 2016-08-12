/*
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
 * Boston, MA  02110-1301  USA
 */

using System;
using System.Collections.Generic;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Natural
{
    /**
    * For each gene of each individual with given probability a mutation occurs.
    * The mutation draws  a number from gaussian distribution with standard 
    * deviation sigma and mean 0, adds it to the value of the gene.
    */
    public class GaussianAdditionMutation : Operator<NaturalVector>
    {
        /** Random number generator. */
        private wevoRandom random;
 
        /** Sigma in gaussian sampling. */
        private double sigma;

        /** Probability of mutation on a single gene. */
        private double probability;

        /**
        * Creates a mutation for natural number individuals. For each gene of each
        * individual with given probability a mutation occurs. The mutation draws 
        * a number from gaussian distribution with standard deviation sigma and
        * mean 0, adds it to the value of the gene
        * and rounds to nearest natural number.
        * @param random Random number genarator.
        * @param probability Probability of mutation for each gene of each individual.
        * @param sigma Standard deviation in gaussian distribution.
        */
        public GaussianAdditionMutation(wevoRandom random, double probability, double sigma)
        {
            this.random = random;
            this.probability = probability;
            this.sigma = sigma;
        }

        public Population<NaturalVector> Apply(Population<NaturalVector> population)
        {
            List<NaturalVector> result = new List<NaturalVector>();
            List<NaturalVector> individuals = population.GetIndividuals();
            foreach (NaturalVector parent in individuals)
            {
                NaturalVector offspring = new NaturalVector(parent.GetSize());
                for (int i = 0; i < offspring.GetSize(); i++)
                {
                    double delta = 0;
                    if (random.NextDouble(0.0, 1.0) < probability)
                    {
                        delta = random.NextGaussian() * sigma;
                    }
                    offspring.SetValue(i, (long)Math.Round(parent.GetValue(i) + delta));
                }
                result.Add(offspring);
            }
            return new Population<NaturalVector>(result);
        }
    }
}
