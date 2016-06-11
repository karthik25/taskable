using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Taskable.Core;
using Taskable.Core.Concrete;
using Taskable.Core.Extensions;
using Taskable.Tests.TestTasks;

namespace Taskable.Tests.ComputedTasks
{
    [TestClass]
    public class TaskFinderTests
    {
        [TestMethod]
        public void CanFindTaskByCommand()
        {
            // create a list
            var rawTasks = new List<ComputedTask>
                           {
                                new ComputedTask(new TestTask()),
                                new ComputedTask(new TestMultipleTask())
                           };

            // create the lookup
            var lookup = rawTasks.ToLookup(k => k.Pattern.Shift(), v => v);

            // call the FindTask method by passing in a command
            var requiredTask = lookup.FindTask(@"move C:\Temp\a.txt to c:\Temp\b.txt");

            // Test for not null etc
            Assert.IsNotNull(requiredTask);
            Assert.AreEqual("TestMultiple", requiredTask.Name);
            Assert.AreEqual("move {} to {}", requiredTask.Pattern);
            Assert.IsNotNull(requiredTask.Data);
            Assert.AreEqual(2, requiredTask.Data.Positions.Count);
            Assert.AreEqual(@"(move)([ ])(\S+)([ ])(to)([ ])(\S+)", requiredTask.Data.Regex.ToString());
            Assert.AreEqual(TaskType.Parameterized, requiredTask.Type);
        }
    }


}
