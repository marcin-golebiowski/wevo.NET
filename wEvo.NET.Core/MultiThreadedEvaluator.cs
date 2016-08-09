using System;
using System.Collections.Generic;

namespace wEvo.NET.Core
{
    public class MultiThreadedEvaluator<T> : PopulationEvaluator<T> where T : Individuals.Individual
    {
        /** Timeout for waiting for executors to compute tasks. */
        private static int TIMEOUT = 3600;

        /** Maximum time to keep alive worker threads. */
        private static int KEEP_ALIVE_TIME = 100;

        /** Maximum size of thread pool. */
        private static int MAXIMUM_POOL_SIZE = 10;

        /**
         * Creates multi-threaded evaluator.
         * @param objectiveFunctions List of objective functions to be evaluated.
         */
        public MultiThreadedEvaluator(List<CachedObjectiveFunction<T>> objectiveFunctions)
        {
        }

        public override void EvaluatePopulation(Population<T> populationInternal)
        {
            throw new NotImplementedException();
        }

        /** Shuts down the evaluator. */
        public void ShutDown()
        {
        }
    }
}
