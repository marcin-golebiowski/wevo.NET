namespace wEvo.NET.Core.Exitcriteria
{
    public class MaxIterations<T> : TerminationCondition<T> where T : Individuals.Individual
    {

        /** Number of iterations. */
        private int maxIter;

        /** Number of current iteration. */
        private int currentIter;

        /** 
         * Creates the termination condition which will succeed after
         * maximum number of iterations has passed.
         * @param maxIter Maximum number of iterations.
         */
        public MaxIterations(int maxIter)
        {
            this.maxIter = maxIter;
        }

        public bool IsSatisfied(Population<T> population)
        {
            currentIter++;
            return currentIter > maxIter;
        }

        public void Reset()
        {
            this.currentIter = 0;
        }

    }
}