using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wevo.NET.Core.Tests
{
    [TestClass]
    public class MultiThreadedEvaluatorTest : PopulationEvaluatorTest
    {
        public override PopulationEvaluator<DummyIndividual> GetEvaluator()
        {
            return new MultiThreadedTaskEvaluator<DummyIndividual>(CreateObjectiveFunctions());
        }

        [TestMethod]
        public void TestObjectiveFunctionsMultiThreaded()
        {
            base.TestObjectiveFunctions();
        }
    }
}
