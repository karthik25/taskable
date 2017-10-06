using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taskable.Core.Parameters;
using TaskableScriptCs.Contracts;

namespace TaskableTests.Parameters
{
    [TestClass]
    public class ActionParameterFluentExtensionTests
    {
        [TestMethod]
        public void CanBindStringsToType1()
        {
            var parameters = new string[] { "somefile.zip", "some/destination" };
            var simpleArgs = parameters.Bind<SimpleArgs>();
            Assert.IsNotNull(simpleArgs);
            Assert.AreEqual("somefile.zip", simpleArgs.SourceUrl);
            Assert.AreEqual("some/destination", simpleArgs.DestinationDirectory);
        }
    }

    public class SimpleArgs
    {
        [ParameterIndex(0)]
        public string SourceUrl { get; set; }
        [ParameterIndex(1)]
        public string DestinationDirectory { get; set; }
    }
}
