using System;
using TaskableScriptCs.Contracts;

namespace TaskableSampleTasks
{
    public class ListAllTasks : ISimpleTask
    {
        public string Pattern
        {
            get
            {
                return "what can i do?";
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
