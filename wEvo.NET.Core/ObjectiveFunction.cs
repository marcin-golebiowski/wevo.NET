namespace wevo.NET.Core
{
    /**
    * Generic interface for a single-criteria objective function.
    * @author Marcin Brodziak (marcin@nierobcietegowdomu.pl)
    *
    * @param <T> Type of the individual being evaluated.
    */
    public interface ObjectiveFunction<T> where T : Individuals.Individual
    {
        /**
         * Computes value of the objective function.
         * @param individual Individual to be evaluated.
         * @return Value of the objective function.
         */
        double Compute(T individual);
    }
}
