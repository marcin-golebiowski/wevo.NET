using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core.Operators.Permutation
{
    class TranspositionMutation : Operator<Individuals.Permutation>
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
        public TranspositionMutation(WevoRandom generator,double probability)
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
