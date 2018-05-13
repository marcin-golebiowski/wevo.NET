using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wevo.NET.Core.Tests
{
    [TestClass]
    public class SingleThreadedEvaluatorTest : PopulationEvaluatorTest
    {
        public override PopulationEvaluator<DummyIndividual> GetEvaluator()
        {
            return new SingleThreadedEvaluator<DummyIndividual>(CreateObjectiveFunctions());
        }

        [TestMethod]
        public void TestObjectiveFunctions()
        {
            base.TestObjectiveFunctions();
        }
    }
}
