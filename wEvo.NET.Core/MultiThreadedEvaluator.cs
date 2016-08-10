using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core
{
    public class MultiThreadedEvaluator<T> : PopulationEvaluator<T> where T : Individuals.Individual
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
        public MultiThreadedEvaluator(List<CachedObjectiveFunction<T>> objectiveFunctions) : base(objectiveFunctions)
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

                        fun.ComputeInternal((T)individual);
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
