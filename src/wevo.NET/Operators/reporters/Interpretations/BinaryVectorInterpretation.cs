using System.Text;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Core.Operators.Reporters.Interpretations
{
    public class BinaryVectorInterpretation : Interpretation<BinaryVector>
    {
        public string Interprete(BinaryVector individual)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < individual.GetSize(); i++)
            {
                builder.Append(individual.GetBit(i) ? "1" : "0");
            }

            builder.AppendLine();

            return builder.ToString();
        }
    }
}
