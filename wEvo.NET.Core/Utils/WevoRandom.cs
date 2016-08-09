using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wEvo.NET.Core.Utils
{
    interface WevoRandom
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
