using System;
using System.Linq;
using ScriptCs.Contracts;
using System.Collections.Generic;
using TaskableCore;
using TaskableCore.Concrete;
using TaskableCore.Extensions;
using TaskableCore.Exceptions;
using TaskableScriptCs.Contracts;

namespace TaskableScriptPack
{
    public class Taskable : IScriptPackContext, ITaskableContext
    {
        private List<ComputedTask> _rawTasks;
        private ILookup<string, ComputedTask> _taskLookup;

        private bool _isInitialized = false;

        private readonly Options _options;

        public Taskable(Options options)
        {
            _options = options;
            _rawTasks = new List<ComputedTask>();
        }

        public void WaitForCommands()
        {
            Initialize();
            "Enter 'bye' or 'q' to exit from the prompt.".PrintGreen();
            _options.ReplPrefix.Print();
            string line = null;
            while (!ExitSignals.Contains(line = Console.ReadLine()))
            {
                var status = InvokeTask(line);
                _options.ReplPrefix.Print();
            }
        }

        public void RegisterTask(ISimpleTask simpleTask)
        {
            if (_isInitialized)
                throw new NotSupportedException("You cannot register tasks after initializing taskable.");

            TaskValidator.ValidateTask(simpleTask);

            var computedTask = new ComputedTask(simpleTask);

            if (_rawTasks.Any(t => t.Name == computedTask.Name))
                throw new DuplicateTaskDefinitionException(computedTask.Name);

            Console.WriteLine("[General] Registered a task: {0}", computedTask.Name);
            _rawTasks.Add(computedTask);
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

        private void Initialize()
        {
            if (!_isInitialized)
            {
                _taskLookup = _rawTasks.ToLookup(k => k.Pattern.Shift(), v => v);
                _isInitialized = true;
                Console.WriteLine("[General] Initialized 'taskable'");
            }
        }

        private static List<string> ExitSignals = new List<string> { "bye", "q" };
    }
}
