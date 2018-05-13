namespace wevo.NET
{
    /**
    * Condition which, when met, signals the {@link Algorithm} to terminate
    * computation. For example it can count the number of iterations of the 
    * algorithm that passed or compute the variance of objective function values
    * in the population. It can be arbitrarily simple or complex. It is executed 
    * only on the <strong>master machine</strong> in master-slave distribution.
    *
    * @param <T> Type of the individual for which condition is evaluated.
    */
    public interface TerminationCondition<T> where T : Individuals.Individual
    {
        bool IsSatisfied(Population<T> population);

        /** Resets termination condition. */
        void Reset();
    }
}
