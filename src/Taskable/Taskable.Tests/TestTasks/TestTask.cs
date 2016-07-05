using System;
using Taskable.ScriptCs.Contracts;

namespace Taskable.Tests.TestTasks
{
    internal class TestTask : ISimpleTask
    {
        public TestTask()
        {
            Name = "Test";
            Pattern = "echo {}";
            Stuff = parameters =>
            {
                Console.WriteLine("Parmeter: " + parameters[0]);
            };
        }

        public TestTask(string name, string pattern)
        {
            Name = name;
            Pattern = pattern;
            Stuff = parameters =>
            {
                Console.WriteLine("Parmeter: " + parameters[0]);
            };
        }

        public TestTask(Action<string[]> action)
        {
            Name = "Test";
            Pattern = "echo {}";
            Stuff = action;
        }

        public string Example
        {
            get
            {
                return "echo Hello!";
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            private set
            {
                _name = value;
            }
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
