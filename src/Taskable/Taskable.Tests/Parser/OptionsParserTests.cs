using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskableTests.Parser
{
    [TestClass]
    public class OptionsParserTests
    {
        [TestMethod]
        public void CanParseAYamlFileIntoOptions()
        {
            const string optionsYaml =
                @"additionalReferences: 
                    - C:\file1.dll
                    - C:\file2.dll
                  taskDefinitionPaths:
                    - C:\Source\SimpleTasks
                    - C:\Source\ComplexTasks";
            var parsedOptions = TaskableCore.Options.ParseFromString(optionsYaml);
            Assert.IsNotNull(parsedOptions);
            Assert.IsNotNull(parsedOptions.TaskDefinitionPaths);
            Assert.AreEqual(2, parsedOptions.TaskDefinitionPaths.Count);
            Assert.AreEqual(@"C:\Source\SimpleTasks", parsedOptions.TaskDefinitionPaths[0]);
            Assert.AreEqual(@"C:\Source\ComplexTasks", parsedOptions.TaskDefinitionPaths[1]);
            Assert.AreEqual(2, parsedOptions.AdditionalReferences);
            Assert.AreEqual(@"C:\file1.dll", parsedOptions.AdditionalReferences[0]);
            Assert.AreEqual(@"C:\file2.dll", parsedOptions.AdditionalReferences[1]);
        }
    }
}
