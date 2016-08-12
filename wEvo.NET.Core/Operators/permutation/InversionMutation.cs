using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core.Operators.Permutation
{
    /**
    * Class implementing a well-known inversion mutation operator. Mutation that
    * takes place is simple:
    * 1) two distinct positions are chosen; let us denote
    * them by i and j. Assume, that i < j.
    * 2) the sequence x_{i}, x_{i+1}, ..., x_{j} is inverted, i.e. we reverse
    * the sequence and copy it to the exact same place in individual.
    * 
    * @author Karol Asgaroth Stosiek (karol.stosiek@gmail.com)
    * @author Szymon Fogiel (szymek.fogiel@gmail.com)
    */
    public class InversionMutation : Operator<Individuals.Permutation>
    {
        /** Probability that a mutation will happen to individual. */
        private double mutationProbability;

        /** Random number generator. */
        private WevoRandom randomGenerator;

        /**
         * Constructor.
         * @param generator Random number generator.
         * @param probability Probability of a individual mutation.
         */
        public InversionMutation(WevoRandom generator, double probability)
        {
            this.randomGenerator = generator;
            this.mutationProbability = probability;
        }

        /** {@inheritDoc} */
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
                    result.AddIndividual(Mutate(individual));
                }
            }

            return result;
        }

        /**
         * Mutates given individual. The source individual stays intact. Package
         * visibility for testing.
         * @param individual Individual to be mutated.
         * @return Mutated individual.
         */
        Individuals.Permutation Mutate(Individuals.Permutation individual)
        {
            Individuals.Permutation mutatedIndividual = new Individuals.Permutation(individual);

            int guess1 = randomGenerator.NextInt(0, individual.GetSize());
            int guess2 = randomGenerator.NextInt(0, individual.GetSize());

            int start = guess1 <= guess2 ? guess1 : guess2;
            int end = guess1 > guess2 ? guess1 : guess2;

            int i = start;
            int j = end;
            while (i <= j)
            {
                mutatedIndividual.Transpose(i++, j--);
            }

            return mutatedIndividual;
        }
    }
}
