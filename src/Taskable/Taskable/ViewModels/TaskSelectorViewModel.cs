﻿using System.Collections.Generic;
using System.Linq;
using TaskableCore;
using TaskableCore.Concrete;
using TaskableRoslynCore;

namespace TaskableApp.ViewModels
{
    public class TaskSelectorViewModel : BindableBase
    {
        private Tasker _tasker;
        private TaskBootstrapper _bootstrapper;

        public List<string> CommandList
        {
            get;set;
        }

        public TaskSelectorViewModel()
        {
            _tasker = Tasker.Instance;
            _bootstrapper = new TaskBootstrapper();
            var tasks = _bootstrapper.GetTasks(_options).Select(t => new ComputedTask(t));
            this.CommandList = tasks.Select(t => t.Command).ToList();
        }
    }
}
