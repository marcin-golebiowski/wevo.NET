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

using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Permutation
{
    class TranspositionMutation : Operator<Individuals.Permutation>
    {
        /** Probability that a mutation will happen to individual. */
        private double mutationProbability;

        /** Random number generator. */
        private wevoRandom randomGenerator;

        /**
         * Constructor.
         * @param generator Random number generator.
         * @param probability Probability of a individual mutation.
         */
        public TranspositionMutation(wevoRandom generator,double probability)
        {
            this.randomGenerator = generator;
            this.mutationProbability = probability;
        }

        public Population<Individuals.Permutation> Apply(Population<Individuals.Permutation> population)
        {
            Population<Individuals.Permutation> result = new Population<Individuals.Permutation>();

            foreach (Individuals.Permutation individual in population.GetIndividuals())
            {
                if (randomGenerator.NextDouble(0.0, 1.0) >= mutationProbability)
                {
                    result.AddIndividual(individual);
                }
                else
                {
                    result.AddIndividual(mutate(individual));
                }
            }

            return result;
        }

        /**
         * Mutates given individual. Package-visibility for testing.
         * @param individual Individual to mutate. Stays intact.
         * @return Mutated individual.
         */
        private Individuals.Permutation mutate(Individuals.Permutation individual)
        {
            Individuals.Permutation mutatedIndividual = new Individuals.Permutation(individual);

            int i = randomGenerator.NextInt(0, individual.GetSize());
            int j = randomGenerator.NextInt(0, individual.GetSize());

            mutatedIndividual.Transpose(i, j);
            return mutatedIndividual;
        }
    }
}
