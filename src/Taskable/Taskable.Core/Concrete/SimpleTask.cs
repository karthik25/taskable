using System;
using TaskableScriptCs.Contracts;

namespace TaskableCore.Concrete
{
    public abstract class SimpleTask : ISimpleTask
    {
        public SimpleTask(ISimpleTask simpleTask)
        {
            this.Pattern = simpleTask.Pattern;
            this.Stuff = simpleTask.Stuff;
        }

        public string Pattern { get; private set; }
        public Action<string[]> Stuff { get; private set; }
    }
}
