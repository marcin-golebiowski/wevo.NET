using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Utils;
using wevo.NET.Core.Individuals;
using Moq;

namespace wevo.NET.Core.Tests.Individuals
{
    [TestClass]
    public class RealVectorTest
    {
        /** Self-explanatory. */
        [TestMethod]
        public void testSetters()
        {
            RealVector vector = new RealVector(2);
            Assert.AreEqual(vector.GetValue(0), 0.0);
            Assert.AreEqual(vector.GetValue(1), 0.0);
            vector.SetValue(0, 1);
            vector.SetValue(1, 3);
            Assert.AreEqual(vector.GetValue(0), 1.0);
            Assert.AreEqual(vector.GetValue(1), 3.0);
        }

        /**
         * Tests if hash code of an individual is exactly the same iff individuals
         * are equal.
         */
        [TestMethod]
        public void testHashCodeConformsEquals()
        {
            RealVector real1 = new RealVector(new double[] { 0.0, 1.1, 2.2 });
            RealVector real2 = new RealVector(new double[] { 0.0, 1.1, 2.2 });
            RealVector real3 = new RealVector(new double[] { 1.0, 2.0, 0.0 });

            Assert.AreEqual(real1.GetHashCode(), real2.GetHashCode());
            Assert.AreEqual(real1, real2);

            Assert.IsFalse(real1.GetHashCode() == real3.GetHashCode());
            Assert.IsFalse(real1.Equals(real3));
        }

        /**
         * Test for individual generation.
         */
        [TestMethod]
        public void testIndividualGenerator()
        {
            var mock = new Mock<wevoRandom>();

            var seq = mock.SetupSequence(m => m.NextDouble(0, 100));
            for (int i = 0; i < 10; i++)
            {
                seq = seq.Returns(i);
            }

            mock.Verify();

            RealVector real = RealVector.Generate(mock.Object, 10, 0, 100);

            for (var i = 0; i < 10; i++)
            {
                Assert.AreEqual((double)i, real.GetValue(i));
            }
        }
    }
}
