using System;
using Taskable.Contracts;

namespace Taskable.Tests.TestTasks
{
    internal class TestMultipleTask : ISimpleTask
    {
        public string Example
        {
            get
            {
                return @"move C:\Temp\a.txt to c:\Temp\b.txt";
            }
        }

        public string Name
        {
            get
            {
                return "TestMultiple";
            }
        }

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
