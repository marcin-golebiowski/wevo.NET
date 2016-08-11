namespace wEvo.NET.Core.Operators
{
    /**
    * Adds a given hardcoded individual to the population.
    * @author Marcin Brodziak (marcin.brodziak@gmail.com)
    * @param <T> type of the individual.
    */
    public class AddHardcodedIndividual<T> : Operator<T> where T : Individuals.Individual
    {   
        /** Factory of individuals to apply. */
        private Factory<T> factory;

        /**
        * Creates the operator.
        * @param factory Factory of hardcoded individuals to apply.
        */
        public AddHardcodedIndividual(Factory<T> factory)
        {
            this.factory = factory;
        }

        public Population<T> Apply(Population<T> population)
        {
            Population<T> result = population;
            result.AddIndividual(factory.Get());
            return result;
        }
    }
}