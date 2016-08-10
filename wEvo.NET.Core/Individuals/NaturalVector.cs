using System;
using System.Text;
using wEvo.NET.Core.Utils;

namespace wEvo.NET.Core.Individuals
{
    class NaturalVector : Individual
    {
        /** Long values in the vector. */
        private long[] values;

        /**
         * Constructs {@link NaturalVector}.
         * @param size Size of the vector.
         */
        public NaturalVector(int size) : this(new long[size])
        {
        }

        /**
         * Constructs NaturalVector from a list of natural values.
         * @param list Natural values that form the basis for this individual.
         */
        public NaturalVector(long[] list)
        {
            values = new long[list.Length];
            for (int i = 0; i < list.Length; i++)
            {
                values[i] = list[i];
            }
        }

        /**
         * Copying constructor.
         * @param individual Individual to be copied.
         */
        public NaturalVector(NaturalVector individual)
        {
            values = individual.values;
        }

        /**
         * Sets the ith value in the vector.
         * @param i Which value to set.
         * @param value What it set to.
         */
        public void SetValue(int i, long value)
        {
            values[i] = value;
        }

        /**
         * Returns ith value in the vector.
         * @param i Which value to return.
         * @return Value of the bit.
         */
        public long GetValue(int i)
        {
            return values[i];
        }

        /**
         * Returns size of the individual.
         * @return Size of the individual.
         */
        public int GetSize()
        {
            return values.Length;
        }

        /** {@inheritDoc} */
        public override int GetHashCode()
        {
            return Arrays<long>.GetHashCode(values);
        }

        /** {@inheritDoc} */
        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is NaturalVector)) {
                return false;
            }

            NaturalVector that = (NaturalVector)obj;
            return Arrays<long>.EqualsArray(this.values, that.values);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            foreach (long b in values)
            {
                if (sb.Length > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(b);
            }
            sb.Append(">");
            return sb.ToString();
        }

        /**
         * Array of values in the individual.
         * @return Array of longs in the individual.
         */
        public long[] GetValues()
        {
            return values;
        }
    }
}
