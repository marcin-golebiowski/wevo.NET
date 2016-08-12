/*
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.

 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor,
   Boston, MA  02110-1301  USA
 */

using System;
using System.Collections.Generic;
using wevo.NET.Core.Utils;

namespace wevo.NET.Core
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
         * Precomputes value of the objective function.
         * @param individual Individual to be evaluated.
         */
        internal void ComputeInternal(T individual)
        {
            if (map.ContainsKey(individual))
            {
                map[individual] = map[individual]; // Update the access time!
                return;
            }
            double v = function.Compute(individual);

            lock (map) {
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
