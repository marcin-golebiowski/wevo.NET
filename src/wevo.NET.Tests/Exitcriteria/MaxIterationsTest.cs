using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Individuals;
using wevo.NET.Exitcriteria;

namespace wevo.NET.Tests.Exitcriteria
{
    [TestClass]
    public class MaxIterationsTest
    {
        [TestMethod]
        public void IsSatifiedTest()
        {
            MaxIterations<BinaryVector> maxIt = new MaxIterations<BinaryVector>(2);
            Assert.IsFalse(maxIt.IsSatisfied(null));
            Assert.IsFalse(maxIt.IsSatisfied(null));
            Assert.IsTrue(maxIt.IsSatisfied(null));
        }
    }
}
