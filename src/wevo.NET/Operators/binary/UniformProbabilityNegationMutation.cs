using System.Collections.Generic;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core.Operators.Binary
{
    /**
    * Performs mutation of the individual by randomly negating one 
    * bit with some probability.
    */
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
