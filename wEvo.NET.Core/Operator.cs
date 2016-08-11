namespace wEvo.NET.Core
{
    /**
    * An operator transforming population of individuals into another
    * population of individuals. Operators can be anything: crossover
    * defined between individuals, mutations on random subsample of them,
    * selection that returns only half of the population, etc. 
    * 
    * A good practice to follow is that it returns a new population, and if the
    * individuals are mutable with copies of the ones that were not changed. 
    * @author Marcin Brodziak (marcin@nierobcietegowdomu.pl)
    *
    * @param <T> Type of the individual in the population.
     */
    public interface Operator<T> where T : Individuals.Individual
    {
        /**
         * Applies the operator to given population. 
         * @param population Source population.
         * @return Population after transformation.
         */
        Population<T> Apply(Population<T> population);
    }
}
