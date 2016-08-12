using wevo.NET.Core;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Samples.ObjectiveFunctions
{
    internal class OneMax : ObjectiveFunction<BinaryVector>
    {
        public double Compute(BinaryVector individual)
        {
            int result = 0;
            int individual_dimension = individual.GetSize();
            for (int i = 0; i < individual_dimension; i++)
            {
                if (individual.GetBit(i))
                {
                    result += 1;
                }
            }
            return result;
        }
    }
}
