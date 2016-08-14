using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Core.Individuals;
using wevo.NET.Core.Exitcriteria;

namespace wevo.NET.Core.Tests.Exitcriteria
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
