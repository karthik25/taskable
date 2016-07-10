using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskableCore.Concrete;
using TaskableCore;
using TaskableTests.TestTasks;
using TaskableCore.Extensions;
using System.Linq;

namespace TaskableTests.ComputedTasks
{
    [TestClass]
    public class ComputedTaskGeneratorTests
    {
        [TestMethod]
        public void GivenAISimpleTaskCanGenerateAComputedTask()
        {
            var testTask = new TestTask();
            var computedTask = new ComputedTask(testTask);
            Assert.IsNotNull(computedTask);
            Assert.IsNotNull(computedTask.Data);
            Assert.AreEqual(1, computedTask.Data.Positions.Count);
            Assert.AreEqual(@"(echo)([ ])("".*?""|\S+)", computedTask.Data.Regex.ToString());
            Assert.AreEqual(TaskType.Parameterized, computedTask.Type);
        }

        [TestMethod]
        public void GivenAISimpleTaskCanGenerateAComputedTaskMultipleParams()
        {
            var testTask = new TestMultipleTask();
            var computedTask = new ComputedTask(testTask);
            Assert.IsNotNull(computedTask);
            Assert.IsNotNull(computedTask.Data);
            Assert.AreEqual(2, computedTask.Data.Positions.Count);
            Assert.AreEqual(@"(move)([ ])("".*?""|\S+)([ ])(to)([ ])("".*?""|\S+)", computedTask.Data.Regex.ToString());
            Assert.AreEqual(TaskType.Parameterized, computedTask.Type);
        }

        [TestMethod]
        public void GivenASimpleTaskCanExtractParameters()
        {
            var command = @"move C:\Temp\a.txt to c:\Temp\b.txt";
            var testTask = new TestMultipleTask();
            var computedTask = new ComputedTask(testTask);
            var parameters = computedTask.GetParameters(command).ToList();
            Assert.AreEqual(2, parameters.Count);
            Assert.AreEqual(@"C:\Temp\a.txt", parameters[0]);
            Assert.AreEqual(@"c:\Temp\b.txt", parameters[1]);
        }

        [TestMethod]
        public void GiveAParameterWithSpaceAndQuotes()
        {
            var command = @"move ""C:\Temp\a.txt"" to ""c:\Temp\b.txt""";
            var testTask = new TestMultipleTask();
            var computedTask = new ComputedTask(testTask);
            var parameters = computedTask.GetParameters(command).ToList();
            Assert.AreEqual(2, parameters.Count);
            Assert.AreEqual(@"""C:\Temp\a.txt""", parameters[0]);
            Assert.AreEqual(@"""c:\Temp\b.txt""", parameters[1]);
        }
    }
}
