using System;
using wEvo.NET.Core.Utils.Superbest_random;

namespace wEvo.NET.Core.Utils
{
    /**
    * Standard random number generator based on .NET random number generator.
    * @author Karol Stosiek (karol.stosiek@gmail.com)
    * @author Michal Anglart (anglart.michal@gmail.com)
    */
    public class dotNetRandom : WevoRandom
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
        public dotNetRandom(WevoClock clock) : this((int)clock.GetCurrentTimeMillis())
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
    }
}
