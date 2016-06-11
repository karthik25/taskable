using System;
using Taskable.Contracts;

namespace Taskable.Core.Concrete
{
    public abstract class SimpleTask : ISimpleTask
    {
        public SimpleTask(ISimpleTask simpleTask)
        {
            this.Name = simpleTask.Name;
            this.Pattern = simpleTask.Pattern;
            this.Stuff = simpleTask.Stuff;
            this.Example = simpleTask.Example;
        }

        public string Name { get; private set; }
        public string Pattern { get; private set; }
        public string Example { get; set; }
        public Action<string[]> Stuff { get; private set; }
    }
}
