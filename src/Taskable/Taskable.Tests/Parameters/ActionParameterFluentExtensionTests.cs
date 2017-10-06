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

        [TestMethod]
        public void CanBindToTypeWithDifferentDataTypes()
        {
            var parameters = new string[] { "1", "2", "some-name" };
            var convertibleArgs = parameters.Bind<ConvertibleArgs>();
            Assert.IsNotNull(convertibleArgs);
            Assert.AreEqual(1, convertibleArgs.Day);
            Assert.AreEqual(2, convertibleArgs.Month);
            Assert.AreEqual("some-name", convertibleArgs.Name);
        }
    }

    public class SimpleArgs
    {
        [ParameterIndex(0)]
        public string SourceUrl { get; set; }
        [ParameterIndex(1)]
        public string DestinationDirectory { get; set; }
    }

    public class ConvertibleArgs
    {
        [ParameterIndex(0)]
        public int Day { get; set; }
        [ParameterIndex(1)]
        public short Month { get; set; }
        [ParameterIndex(2)]
        public string Name { get; set; }
    }
}
