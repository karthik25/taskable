using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Taskable.Tests.Parser
{
    [TestClass]
    public class OptionsParserTests
    {
        [TestMethod]
        public void CanParseAYamlFileIntoOptions()
        {
            const string optionsYaml = @"";
            var parsedOptions = Core.Options.ParseFromFile(optionsYaml);
            Assert.IsNotNull(parsedOptions);
            Assert.AreEqual("d", parsedOptions.LogLevel);
            Assert.AreEqual(">>>", parsedOptions.CommandPrefix);
            Assert.IsNotNull(parsedOptions.TaskDefinitionPaths);
            Assert.AreEqual(2, parsedOptions.TaskDefinitionPaths.Count);
            Assert.AreEqual(@"C:\Source\SimpleTasks", parsedOptions.TaskDefinitionPaths[0]);
            Assert.AreEqual(@"C:\Source\ComplexTasks", parsedOptions.TaskDefinitionPaths[1]);
        }
    }
}
