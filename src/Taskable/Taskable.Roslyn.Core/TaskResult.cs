using System.Collections.Generic;
using TaskableScriptCs.Contracts;

namespace TaskableRoslynCore
{
    public class TaskResult
    {
        public TaskResult()
        {
            this.Tasks = new List<ISimpleTask>();
        }

        public List<ISimpleTask> Tasks { get; set; }
        public List<string> Errors { get; set; }
    }
}
