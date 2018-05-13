using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Operators;

namespace wevo.NET.Core.Tests.Operators
{
    /// <summary>
    /// Summary description for BestFractionSelectionTest
    /// </summary>
    [TestClass]
    public class BestFractionSelectionTest
    {
        [TestMethod]
        public void TestOperator()
        {
            List<BinaryVector> vectors = new List<BinaryVector>();
            vectors.Add(new BinaryVector(new bool[] { true, false }));
            vectors.Add(new BinaryVector(new bool[] { true, true, true }));

            Population<BinaryVector> p = new Population<BinaryVector>(vectors);
            BestFractionSelection<BinaryVector> op = new BestFractionSelection<BinaryVector>((BinaryVector v) => { return v.GetSize(); }, 0.5);
            p = op.Apply(p);

            Assert.AreEqual(p.GetIndividuals().Count, 2);
            Assert.AreEqual(p.GetIndividuals()[0], new BinaryVector(new bool[] { true, true, true }));
            Assert.AreEqual(p.GetIndividuals()[1], new BinaryVector(new bool[] { true, true, true }));
        }
    }
}
