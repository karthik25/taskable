using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskableCore;
using TaskableCore.Exceptions;
using TaskableTests.TestTasks;

namespace TaskableTests.ComputedTasks
{
    [TestClass]
    public class TaskValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidTaskDefinitionException))]
        public void CanThrowAnExceptionOnEmptyParameters()
        {
            var simpleTask = new TestTask("");
            TaskValidator.ValidateTask(simpleTask);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTaskDefinitionException))]
        public void CanThrowAnExceptionOnNullStuff()
        {
            var simpleTask = new TestTask(null, "echo {}");
            TaskValidator.ValidateTask(simpleTask);
        }
    }
}
