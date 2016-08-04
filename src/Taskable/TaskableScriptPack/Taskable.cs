using System;
using ScriptCs.Contracts;
using System.Collections.Generic;
using TaskableCore;
using TaskableCore.Extensions;
using TaskableScriptCs.Contracts;

namespace TaskableScriptPack
{
    public class Taskable : IScriptPackContext, ITaskableContext
    {
        private Tasker _tasker;
        private readonly Arguments _arguments;
        private readonly string _replPrefix = "> ";

        public Taskable(Arguments arguments)
        {
            _arguments = arguments;
            _tasker = Tasker.Instance;
        }

        public void WaitForCommands()
        {
            var init = _tasker.Initialize();
            "Initialized taskable".PrintGreen();
            "Enter 'bye' or 'q' to exit from the prompt.".PrintRed();
            _replPrefix.Print();
            string line = null;
            while (!ExitSignals.Contains(line = Console.ReadLine()))
            {
                var status = _tasker.InvokeTask(line);
                _replPrefix.Print();
            }
        }

        public void RegisterTask(ISimpleTask simpleTask)
        {
            var computedTask = _tasker.RegisterTask(simpleTask);
            string.Format("Registered a task: {0}", computedTask.Command).PrintYellow();
        }

        private static List<string> ExitSignals = new List<string> { "bye", "q" };
    }
}
