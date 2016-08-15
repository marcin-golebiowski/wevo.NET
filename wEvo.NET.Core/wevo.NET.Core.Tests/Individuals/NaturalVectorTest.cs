using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;

namespace wevo.NET.Core.Tests.Individuals
{
    [TestClass]
    public class NaturalVectorTest
    {
        public void TestMethod1()
        {
        }

        /** Self-explanatory. */
        [TestMethod]
        public void testSetters()
        {
            // MagicNumber off 
            NaturalVector vector = new NaturalVector(2);
            Assert.AreEqual(vector.GetValue(0), 0);
            Assert.AreEqual(vector.GetValue(1), 0);
            vector.SetValue(0, 1);
            vector.SetValue(1, 3);
            Assert.AreEqual(vector.GetValue(0), 1);
            Assert.AreEqual(vector.GetValue(1), 3);
            // MagicNumber on
        }

        /**
         * Tests if hash code of an individual is exactly the same iff individuals
         * are equal.
         */
        [TestMethod]
        public void testHashCodeConformsEquals()
        {
            NaturalVector natural1 = new NaturalVector(new long[] { 0, 1, 2 });
            NaturalVector natural2 = new NaturalVector(new long[] { 0, 1, 2 });
            NaturalVector natural3 = new NaturalVector(new long[] { 1, 2, 0 });

            Assert.AreEqual(natural1.GetHashCode(), natural2.GetHashCode());
            Assert.AreEqual(natural1, natural2);

            Assert.IsFalse(natural1.GetHashCode() == natural3.GetHashCode());
            Assert.IsFalse(natural1.Equals(natural3));
        }
    }
}
