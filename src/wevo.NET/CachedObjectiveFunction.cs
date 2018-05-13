using System;
using System.Collections.Generic;
using wevo.NET.Utils;

namespace wevo.NET
{
    /**
    * Wrapper for an objective function that caches the result of computation.
    *
    * @param <T> Type of the individual being evaluated.
    */
    public class CachedObjectiveFunction<T> where T : Individuals.Individual
    {
        /** Parent objective function that will be cached. */
        private ObjectiveFunction<T> function;

        /** Map &mdash; cache from individual to its value. */
        private LruMap<T, Double> map;

        /**
        * Creates caching wrapper for given objective function.
        * @param function Objective function to be cached.
        * @param cacheSize Size of the cache.
        */   
        public CachedObjectiveFunction(ObjectiveFunction<T> function, int cacheSize)
        {
            this.function = function;
            map = new LruMap<T, Double>(cacheSize);
        }

        /**
        * Creates caching wrapper for given objective function.
        * @param function Objective function to be cached.
        * @param cacheSize Size of the cache.
        */
        public CachedObjectiveFunction(IObjectiveFunction<T> function, int cacheSize)
        {
            this.function = function.Compute;
            map = new LruMap<T, Double>(cacheSize);
        }

        /**
         * Precomputes value of the objective function.
         * @param individual Individual to be evaluated.
         */
        public void Precompute(T individual)
        {
            lock (map)
            {
                if (map.ContainsKey(individual))
                {
                    map[individual] = map[individual]; // Update the access time!
                    return;
                }
                double v = function(individual);
                map.Add(individual, v);
            }
        }

        public double Compute(T o)
        {
            if (!map.ContainsKey(o))
            {
                throw new Exception("Cache of objective function values does not contain entry for " + o);
            }
            return map[o];
        }

        /**
         * Merges this function's cache with cache of the argument.
         * @param input Objective function that contains the cache
         * to merge.
         */
        public void Merge(Dictionary<T, Double> input)
        {
            foreach (T key in input.Keys)
            {
                map.Add(key, input[key]);
            }
        }
    }
}
