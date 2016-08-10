using System.Collections.Generic;

namespace wEvo.NET.Core
{
    class SingleThreadedEvaluator<T> : PopulationEvaluator<T> where T : Individuals.Individual
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
