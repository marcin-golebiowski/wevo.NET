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
   Boston, MA  02110-1301  USA
 */

/**
 * Performs mutation of the individual by randomly negating one 
 * bit with some probability.
 */
using System.Collections.Generic;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Binary
{
    public class UniformProbabilityNegationMutation : Operator<BinaryVector>
    {
        /** For each bit, probability of negating it. */
        private double mutationProbability;

        /** Random number generator. */
        private wevoRandom random;

        /**
        * Returns a mutator, which modifies each bit of the {@link BinaryVector}
        * negated with some probability.
        * @param mutationProbability Probability of the mutation of each bit 
           of the individual.
        * @param random Random number generator.
        */
        public UniformProbabilityNegationMutation(double mutationProbability, wevoRandom random)
        {
            this.mutationProbability = mutationProbability;
            this.random = random;
        }

        public Population<BinaryVector> Apply(Population<BinaryVector> population)
        {
            var enumerator = population.GetIndividuals().GetEnumerator();
            List<BinaryVector> output = new List<BinaryVector>();

            while (enumerator.MoveNext())
            {
                BinaryVector binaryIndividual = enumerator.Current;

                if (random.NextDouble(0.0, 1.0) < mutationProbability)
                {
                    binaryIndividual.NegateBit(random.NextInt(0, binaryIndividual.GetSize()));
                }
                output.Add(binaryIndividual);
            }
            return new Population<BinaryVector>(output);
        }
    }
}
