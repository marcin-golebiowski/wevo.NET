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

namespace wevo.NET.Core.Utils
{
    /**
    * Interface for random number generators.
    */
    public interface wevoRandom
    {
        /**
        * Returns next random long number.
        * @param lowerLimit Lower limit for the result (treated inclusively);
        * must be non-negative.
        * @param upperLimit Upper limit for the result (treated exclusively);
        * must be non-negative.
        * @return Random long number.
        */
        long NextLong(long lowerLimit, long upperLimit);

        /**
         * Returns next random integer.
         * @param lowerLimit Lower limit for the result (treated inclusively);
         * must be non-negative.
         * @param upperLimit Upper limit for the result (treated exclusively);
         * must be non-negative.
         * @return Random integer.
         */
        int NextInt(int lowerLimit, int upperLimit);

        /**
         * Returns next random double. Lower limit must be different than upper limit!
         * @param lowerLimit Lower limit for the result (treated inclusively).
         * @param upperLimit Upper limit for the result (treated exclusively).
         * @return Random double.
         */
        double NextDouble(double lowerLimit, double upperLimit);

        /**
         * Returns next random boolean.
         * @return Random boolean.
         */
        bool NextBoolean();

        /**
         * Returns inner random number generator, if any used.
         * @return Random generator used for generating random numbers
         * or null, if such doesn't exist.
         */
        Random GetInnerGenerator();

        /**
         * Returns next pseudorandom value according to Gaussian distribution.
         * @return Next pseudorandom value according to Gaussian distribution.
         */
        double NextGaussian();
    }
}
