using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core
{
    /**
    * Implements multi-threaded evaluator. By far this is the one you should be 
    * using if you are writing a software for a single, but multi-core machine. 
    * 
    * @param <T> Type of individuals to be evaluated.
    */
    public class MultiThreadedTaskEvaluator<T> : PopulationEvaluator<T> where T : Individuals.Individual
    {
        /** Timeout for waiting for executors to compute tasks. */
        private static int TIMEOUT = 1000 * 10;

        /** Maximum size of thread pool. */
        private static int MAXIMUM_POOL_SIZE = 10;

        private List<Task> tasks;
        private CancellationTokenSource cts;

        /**
         * Creates multi-threaded evaluator.
         * @param objectiveFunctions List of objective functions to be evaluated.
         */
        public MultiThreadedTaskEvaluator(List<CachedObjectiveFunction<T>> objectiveFunctions) : base(objectiveFunctions)
        {
        }

        public override void EvaluatePopulation(Population<T> populationInternal)
        {
            tasks = new List<Task>();
            cts = new CancellationTokenSource();

            TaskFactory factory = new TaskFactory(new LimitedConcurrencyLevelTaskScheduler(MAXIMUM_POOL_SIZE));

            foreach (Individuals.Individual individual in populationInternal.GetIndividuals())
            {
                Task t = factory.StartNew(() =>
                {
                    foreach (CachedObjectiveFunction<T> fun in GetObjectiveFunctions())
                    {
                        if (cts.Token.IsCancellationRequested)
                        {
                            break;
                        }

                        fun.Precompute((T)individual);
                    }
                },
                cts.Token);

                tasks.Add(t);
            }

            Task.WaitAll(tasks.ToArray(), TIMEOUT);
        }

        /** Shuts down the evaluator. */
        public void ShutDown()
        {
            cts.Cancel();
        }
    }
}
