using System;
using System.Collections.Generic;
using System.Linq;
using TaskableCore.Concrete;
using TaskableCore.Exceptions;
using TaskableCore.Extensions;
using TaskableScriptCs.Contracts;

namespace TaskableCore
{
    public sealed class Tasker
    {
        private static volatile Tasker tasker;
        private static object rootObj = new object();

        private List<ComputedTask> _rawTasks;
        private ILookup<string, ComputedTask> _taskLookup;
        private bool _isInitialized = false;
        private CommandExecutionFactory _commandExecutionFactory;

        private Tasker()
        {
            _rawTasks = new List<ComputedTask>();
        }

        public static Tasker Instance
        {
            get
            {
                if (tasker == null)
                {
                    lock (rootObj)
                    {
                        if (tasker == null)
                        {
                            tasker = new Tasker();
                        }
                    }
                }
                return tasker;
            }
        }

        public ComputedTask RegisterTask(ISimpleTask simpleTask)
        {
            if (_isInitialized)
                throw new NotSupportedException("You cannot register tasks after initializing taskable.");

            TaskValidator.ValidateTask(simpleTask);

            var computedTask = new ComputedTask(simpleTask);

            if (_rawTasks.Any(t => t.Name == computedTask.Name))
                throw new DuplicateTaskDefinitionException(computedTask.Name);

            _rawTasks.Add(computedTask);

            return computedTask;
        }

        public bool InvokeTask(string command)
        {
            TaskerCommand taskerCommand;
            if (command.IsCommand(out taskerCommand))
            {
                var commandParameters = command.GetCommandParameters();
                _commandExecutionFactory.Execute(taskerCommand, commandParameters);
                return true;
            }

            var task = _taskLookup.FindTask(command);
            if (task != null)
            {
                var parameters = task.GetParameters(command);
                task.Stuff(parameters.ToArray());
                return true;
            }
            return false;
        }

        public bool Initialize()
        {
            if (!_isInitialized)
            {
                _taskLookup = _rawTasks.ToLookup(k => k.Pattern.Shift(), v => v);
                var commandContext = CreateCommandContext();
                _commandExecutionFactory = CommandExecutionFactory.Create(commandContext);
                _isInitialized = true;
                return _isInitialized;
            }
            return false;
        }

        public ComputedTask FindTask(string commandPrefix)
        {
            if (!_isInitialized)
                throw new Exception("Tasker has not been initialized");

            if (!_taskLookup.Contains(commandPrefix))
                throw new Exception(string.Format("Task {0} is unknown or not registered", commandPrefix));

            return _taskLookup[commandPrefix].First();
        }

        public IEnumerable<string> GetTaskCommands()
        {
            return _taskLookup.Select(t => t.Key);
        }

        private CommandContext CreateCommandContext()
        {
            return new CommandContext
            {
                TaskLookup = _taskLookup
            };
        }
    }          
}
