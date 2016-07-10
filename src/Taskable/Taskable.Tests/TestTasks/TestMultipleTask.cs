using System;
using TaskableScriptCs.Contracts;

namespace TaskableTests.TestTasks
{
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
