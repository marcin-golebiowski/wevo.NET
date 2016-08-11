using System.Collections.Generic;

namespace wEvo.NET.Core
{
    /**
    * A single-threaded evaluator. Iterates in a loop over all individual in 
    * a population and computes the value of objective function.
    * 
    * @author Marcin Brodziak (marcin@nierobcietegowdomu.pl)
    *
    * @param <T> Type of the individual being evaluated.
    */
    public class SingleThreadedEvaluator<T> : PopulationEvaluator<T> where T : Individuals.Individual
    {
        /**
        * Creates single threaded evaluator.
        * @param objectiveFunctions List of the objective functions to be evaluated.
        */
        public SingleThreadedEvaluator(List<CachedObjectiveFunction<T>> objectiveFunctions) : base(objectiveFunctions)
        {
        }

        public override void EvaluatePopulation(Population<T> populationInternal)
        {
            foreach (T individual in populationInternal.GetIndividuals())
            {
                foreach (CachedObjectiveFunction<T> objectiveFunction in GetObjectiveFunctions())
                {
                    objectiveFunction.ComputeInternal(individual);
                }
            }
        }
    }
}
