using System;
using System.Linq;
using ScriptCs.Contracts;
using System.Collections.Generic;
using Taskable.Core;
using Taskable.Contracts;
using Taskable.Core.Concrete;
using Taskable.Core.Extensions;
using Taskable.Core.Exceptions;

namespace Taskable
{
    public class TaskableContext : IScriptPackContext, ITaskableContext
    {
        private List<ComputedTask> _rawTasks;
        private ILookup<string, ComputedTask> _taskLookup;

        private bool _isInitialized = false;

        private readonly Options _options;

        public TaskableContext(Options options)
        {
            _options = options;
            _rawTasks = new List<ComputedTask>();
        }

        public void Initialize()
        {
            if (!_isInitialized)
            {
                _taskLookup = _rawTasks.ToLookup(k => k.Pattern.Shift(), v => v);
                _isInitialized = true;
            }
        }

        public void WaitForCommands()
        {
            _options.ReplPrefix.PrintDefault();
            string line = null;
            while (!ExitSignals.Contains(line = Console.ReadLine()))
            {
                var status = InvokeTask(line);
                _options.ReplPrefix.PrintDefault();
            }
        }

        public void RegisterTask(ISimpleTask simpleTask)
        {
            if (_isInitialized)
                throw new NotSupportedException("You cannot register tasks after initializing taskable.");

            TaskValidator.ValidateTask(simpleTask);

            if (_rawTasks.Any(t => t.Name == simpleTask.Name))
                throw new DuplicateTaskDefinitionException(simpleTask.Name);

            _rawTasks.Add(new ComputedTask(simpleTask));
        }

        private bool InvokeTask(string command)
        {
            var task = _taskLookup.FindTask(command);
            if (task != null)
            {
                var parameters = task.GetParameters(command);
                task.Stuff(parameters.ToArray());
                return true;
            }
            return false;
        }

        private static List<string> ExitSignals = new List<string> { "bye", "q" };
    }
}
