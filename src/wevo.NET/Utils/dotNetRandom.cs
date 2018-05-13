/*
 * wevo.NET - Distributed Evolutionary Computation Library
 *
 * Based on wevo and wevo2 libraries.
 *
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
using wevo.NET.Core.Utils.Superbest_random;

namespace wevo.NET.Core.Utils
{
    /**
    * Standard random number generator based on .NET random number generator.
    */
    public class dotNetRandom : wevoRandom
    {
        /** Random number generator. */
        private Random generator;

        /** Default constructor using current time as seed. */
        public dotNetRandom() : this(new SystemClock())
        {
        }

        /**
         * Constructor using current time as seed.
         * @param clock Time measurement utility.
         */
        public dotNetRandom(wevoClock clock) : this((int)clock.GetCurrentTimeMillis())
        {
        }

        /**
         * Constructor with seed.
         * @param seed Seed to the random number generator.
         */
        public dotNetRandom(int seed)
        {
            this.generator = new Random(seed);
        }

        public Random GetInnerGenerator()
        {
            return generator;
        }

        public bool NextBoolean()
        {
            return generator.NextBoolean();
        }

        public double NextDouble(double lowerLimit, double upperLimit)
        {
            return generator.NextDouble() * (upperLimit - lowerLimit) + lowerLimit;
        }

        public double NextGaussian()
        {
            return generator.NextGaussian();
        }

        public int NextInt(int lowerLimit, int upperLimit)
        {
            return generator.Next(lowerLimit, upperLimit);
        }

        public long NextLong(long lowerLimit, long upperLimit)
        {
            return generator.Next((int)lowerLimit, (int)upperLimit);
        }

        public bool NextProbableBoolean(double probability)
        {
            if (NextDouble(0, 1) < probability)
                return true;
            return false;
        }
    }
}
