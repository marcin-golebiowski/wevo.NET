using System.Collections.Generic;

namespace wEvo.NET.Core
{
    public abstract class PopulationEvaluator<T> : Operator<T> where T : Individuals.Individual
    {
       /**
       * List of objective functions that will be evaluated.
       */

        // VisibilityModifier off
        protected List<CachedObjectiveFunction<T>> objectiveFunctions;
        // VisibilityModifier on

        /**
         * Creates population evaluator.
         * @param objectiveFunctions List of objective functions
         *    that will be evaluated.
         */
        protected PopulationEvaluator(List<CachedObjectiveFunction<T>> objectiveFunctions)
        {
            this.objectiveFunctions = objectiveFunctions;
        }

        public Population<T> Apply(Population<T> populationInternal)
        {
            EvaluatePopulation(populationInternal);
            return populationInternal;
        }

        /**
         * Returns list of objective functions to be evaluated.
         * @return List of objective functions to be evaluated.
         */
        public List<CachedObjectiveFunction<T>> GetObjectiveFunctions()
        {
            return objectiveFunctions;
        }

        /**
         * Evaluates given population.
         * @param populationInternal Population to be evaluated.
         */
        public abstract void EvaluatePopulation(Population<T> populationInternal);
    }
}
