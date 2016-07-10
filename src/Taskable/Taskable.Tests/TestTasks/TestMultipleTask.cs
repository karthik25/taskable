using System;
using TaskableCore.Attributes;
using TaskableScriptCs.Contracts;

namespace TaskableTests.TestTasks
{
    [TaskName("Test Multiple")]
    [TaskExample(@"move C:\Temp\a.txt to c:\Temp\b.txt")]
    internal class TestMultipleTask : ISimpleTask
    {
        public string Pattern
        {
            get
            {
                return "move {} to {}";
            }
        }

        public Action<string[]> Stuff
        {
            get
            {
                return parameters =>
                {
                    Console.WriteLine("Parmeter: " + parameters[0]);
                    Console.WriteLine("Parmeter: " + parameters[1]);
                };
            }
        }
    }
}
