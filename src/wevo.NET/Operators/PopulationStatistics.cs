namespace wevo.NET.Operators
{
    /**
    * Tracks some statistics of population.
    *
    * @param <T> Type of the individual in the population.
    */
    public class PopulationStatistics<T> where T : Individuals.Individual
    {
        /** Objective functions used in evaluation. */
        private ObjectiveFunction<T> objectiveFunction;

        /** Current best individual. */
        private T currentBest;

        /** Current best individual value. */
        private double currentAverageValue;

        /** Current best individual. */
        private T currentWorst;

        /**
         * Constructor.
         * @param function Function that is used for comparison.
         */
        public PopulationStatistics(ObjectiveFunction<T> function)
        {
            this.objectiveFunction = function;
        }

        public Population<T> Apply(Population<T> population)
        {
            int i = 0;
            currentAverageValue = 0.0;
            if (currentBest == null && currentWorst == null)
            {
                T first = population.GetIndividuals()[0];
                currentBest = first;
                currentWorst = first;
                currentAverageValue = objectiveFunction(first);
                i++;
            }

            while (i < population.Size())
            {
                T currentCandidate = population.GetIndividuals()[i];
                double v1 = objectiveFunction(currentCandidate);
                double v2 = objectiveFunction(currentBest);
                double v3 = objectiveFunction(currentWorst);

                currentAverageValue += v1;
                if (v1 > v2)
                {
                    currentBest = currentCandidate;
                }
                if (v1 < v3)
                {
                    currentWorst = currentCandidate;
                }
                i++;
            }
            currentAverageValue /= population.Size();
            return population;
        }

        /**
         * Returns current best individual in the population.
         * @return Best yet found individual or null if none exists.
         */
        public T GetBestIndividual()
        {
            return currentBest;
        }

        /**
         * Returns currently best individual objective value.
         * @return Value of the currently best individual.
         */
        public double GetBestIndividualValue()
        {
            return objectiveFunction(currentBest);
        }

        /**
         * Returns current worst individual in the population.
         * @return Worst yet found individual or null if none exists.
         */
        public T GetWorstIndividual()
        {
            return currentWorst;
        }

        /**
         * Returns currently worst individual objective value.
         * @return Value of the currently worst individual.
         */
        public double GetWorstIndividualValue()
        {
            return objectiveFunction(currentWorst);
        }

        /**
         * Returns current average individual objective value.
         * @return Value of the current average individual.
         */
        public double getAverageIndividualValue()
        {
            return currentAverageValue;
        }
    }
}
