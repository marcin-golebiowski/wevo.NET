using System;

namespace wevo.NET.Operators.Reporters
{
    /**
    * A interface for providing an interpretation to an individual.
    * @param <T> type of the individual to be interpreted.
    */
    public interface Interpretation<T>
    {

        /**
         * Takes an individual and returns a human-friendly interpretation of it.
         * @param individual Individual to be interpreted.
         * @return String based interpretation of the individual.
         */
        String Interprete(T individual);
    }
}
