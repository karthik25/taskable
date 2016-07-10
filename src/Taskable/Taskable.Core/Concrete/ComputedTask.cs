﻿using System.Collections.Generic;
using TaskableCore.Extensions;
using TaskableScriptCs.Contracts;

namespace TaskableCore.Concrete
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

        public string Name { get; set; }
        public IEnumerable<string> Examples { get; set; }
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
