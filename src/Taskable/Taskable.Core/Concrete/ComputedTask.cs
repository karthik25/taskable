using Taskable.Core.Extensions;
using Taskable.ScriptCs.Contracts;

namespace Taskable.Core.Concrete
{
    public class ComputedTask : SimpleTask
    {
        public ComputedTask(ISimpleTask simpleTask)
            :base (simpleTask)
        {
            this.Compute();
        }

        private void Compute()
        {
            this.Data = this.Pattern.Parse();
        }

        public ParameterData Data { get; set; }
        public TaskType Type
        {
            get
            {
                return Data.Positions.Count == 0 ? TaskType.Simple : TaskType.Parameterized;
            }
        }
    }
}
