using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taskable.Core;
using Taskable.Core.Exceptions;
using Taskable.Tests.TestTasks;

namespace Taskable.Tests.ComputedTasks
{
    [TestClass]
    public class TaskValidatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidTaskDefinitionException))]
        public void CanThrowAnExceptionOnEmptyName()
        {
            var simpleTask = new TestTask("", "echo {}");
            TaskValidator.ValidateTask(simpleTask);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTaskDefinitionException))]
        public void CanThrowAnExceptionOnEmptyParameters()
        {
            var simpleTask = new TestTask("Test", "");
            TaskValidator.ValidateTask(simpleTask);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidTaskDefinitionException))]
        public void CanThrowAnExceptionOnNullStuff()
        {
            var simpleTask = new TestTask(null);
            TaskValidator.ValidateTask(simpleTask);
        }
    }
}
