
using wevo.NET.Core;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Samples.ObjectiveFunctions
{
    internal class OneMax
    {
        public static double Compute(BinaryVector individual)
        {
            int result = 0;
            int individual_dimension = individual.GetSize();
            for (int i = 0; i < individual_dimension; i++)
            {
                if (individual.GetBit(i))
                {
                    if (i % 2 == 0)
                    {
                        result += 1;
                    }
                    else
                    {
                        result -= 1;

                    }

                }
                else
                {
                    if (i % 2 == 1)
                    {
                        result += 1;
                    }
                    else
                    {
                        result -= 1;

                    }
                }
            }
            return result;
        }
    }
}
