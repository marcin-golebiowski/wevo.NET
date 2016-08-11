using System;

namespace wEvo.NET.Core.Utils
{
    class dotNetRandom : WevoRandom
    {
        public Random GetInnerGenerator()
        {
            throw new NotImplementedException();
        }

        public bool NextBoolean()
        {
            throw new NotImplementedException();
        }

        public double NextDouble(double lowerLimit, double upperLimit)
        {
            throw new NotImplementedException();
        }

        public double NextGaussian()
        {
            throw new NotImplementedException();
        }

        public int NextInt(int lowerLimit, int upperLimit)
        {
            throw new NotImplementedException();
        }

        public long NextLong(long lowerLimit, long upperLimit)
        {
            throw new NotImplementedException();
        }
    }
}
