﻿using System;
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
            var commandContext = CreateCommandContext();
            _commandExecutionFactory = CommandExecutionFactory.Create(commandContext);
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

        public bool InvokeTask(string command)
        {
            TaskerCommand taskerCommand;
            if (command.IsCommand(out taskerCommand))
            {
                Console.WriteLine("[Command] Executing...");
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

        public void Initialize()
        {
            if (!_isInitialized)
            {
                _taskLookup = _rawTasks.ToLookup(k => k.Pattern.Shift(), v => v);
                _isInitialized = true;
                Console.WriteLine("[General] Initialized 'taskable'");
            }
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