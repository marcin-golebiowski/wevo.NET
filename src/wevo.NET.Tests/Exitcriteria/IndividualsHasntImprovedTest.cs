using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wevo.NET.Exitcriteria;
using System.Collections.Generic;
using wevo.NET.Individuals;

namespace wevo.NET.Tests.Exitcriteria
{
    [TestClass]
    public class IndividualsHasntImprovedTest
    {
        [TestMethod]
        public void IndividualHasntChanged()
        {
            IndividualHasntImproved<BinaryVector> maxIt = new IndividualHasntImproved<BinaryVector>(2, (BinaryVector v) => { return v.GetSize();  });

            List<BinaryVector> list = new List<BinaryVector>();
            list.Add(new BinaryVector(10));

            Population<BinaryVector> population = new Population<BinaryVector>(list);

            // Two false assertions
            Assert.IsFalse(maxIt.IsSatisfied(population));
            Assert.IsFalse(maxIt.IsSatisfied(population));
            Assert.IsTrue(maxIt.IsSatisfied(population));


            // New good individual popped up
            population.AddIndividual(new BinaryVector(11));

            // Two more hopeful run
            Assert.IsFalse(maxIt.IsSatisfied(population));
            Assert.IsFalse(maxIt.IsSatisfied(population));
            // Finaly exiting
            Assert.IsTrue(maxIt.IsSatisfied(population));
        }
    }
}
