using System;
using Taskable.Contracts;

namespace Taskable.SampleTasks
{
    public class ListAllTasks : ISimpleTask
    {
        public string Example
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "List all tasks";
            }
        }

        public string Pattern
        {
            get
            {
                return "what can i do?";
            }
        }

        public Action<string> Stuff
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        Action<string[]> ISimpleTask.Stuff
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
