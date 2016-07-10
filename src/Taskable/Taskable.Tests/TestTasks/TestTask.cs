using System;
using TaskableScriptCs.Contracts;

namespace TaskableTests.TestTasks
{
    internal class TestTask : ISimpleTask
    {
        public TestTask()
        {
            Pattern = "echo {}";
            Stuff = parameters =>
            {
                Console.WriteLine("Parmeter: " + parameters[0]);
            };
        }

        public TestTask(string pattern)
        {
            Pattern = pattern;
            Stuff = parameters =>
            {
                Console.WriteLine("Parmeter: " + parameters[0]);
            };
        }

        public TestTask(Action<string[]> action, string pattern)
        {
            Pattern = pattern;
            Stuff = action;
        }

        private string _pattern;
        public string Pattern
        {
            get
            {
                return _pattern;
            }
            private set
            {
                _pattern = value;
            }
        }

        private Action<string[]> _stuff;
        public Action<string[]> Stuff
        {
            get
            {
                return _stuff;
            }
            private set
            {
                _stuff = value;
            }
        }
    }
}
