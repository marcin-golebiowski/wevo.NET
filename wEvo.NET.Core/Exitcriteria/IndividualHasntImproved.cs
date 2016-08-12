namespace wEvo.NET.Core.Exitcriteria
{
    /**
    * Terminates the evolution after for given number of iterations the
    * individual hasn't changed.
    *  
    * @author Marcin Brodziak (marcin@nierobcietegowdomu.pl)
    *
    * @param <T> Type of the individual for which it is evaluated.
    */
    public class IndividualHasntImproved<T> : TerminationCondition<T> where T : Individuals.Individual
    {
        /** Number of iterations the individual is allowed to not change. */
        private int maxIter;

        /** Best individual so far. */
        private T individual;

        /** Number of iterations individual hasn't changed. */
        private int iterationsWithNoChange;

        /** Objective function to look at when finding best individual. */
        private ObjectiveFunction<T> objFunction;

        /** 
         * Creates the termination condition which will succeed after
         * maximum number of iterations has passed.
         * @param maxIter Maximum number of iterations.
         * @param objFunction Objective function to look at.
         */
        public IndividualHasntImproved(int maxIter,
            ObjectiveFunction<T> objFunction)
        {
            this.maxIter = maxIter;
            this.objFunction = objFunction;
        }

        public bool IsSatisfied(Population<T> population)
        {
            var individuals = population.GetIndividuals();
            individuals.Sort((T o1, T o2) => objFunction.Compute(o1) > objFunction.Compute(o2) ? 1 : -1);

            if (individuals.Count == 0)
            {
                throw new System.Exception("Population is empty");
            }

            T bestIndividual = individuals[0];

            if (individual != null && objFunction.Compute(bestIndividual) <= objFunction.Compute(individual))
            {
                iterationsWithNoChange++;
            }
            else
            {
                individual = bestIndividual;
                iterationsWithNoChange = 0;
            }
            return iterationsWithNoChange > maxIter - 1;
        }

        public void Reset()
        {
            iterationsWithNoChange = 0;
        }
    }
}
