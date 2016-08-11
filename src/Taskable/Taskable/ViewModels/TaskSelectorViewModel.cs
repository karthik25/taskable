using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using TaskableApp.Models;
using TaskableCore;
using TaskableCore.Concrete;
using TaskableRoslynCore;
using TaskableScriptCs.Contracts;

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

        public string SelectedTask { get; set; }

        public ObservableCollection<ParameterItemViewModel> Parameters
        {
            get;set;
        }

        public ObservableCollection<Error> Errors
        {
            get;set;
        }

        public AddParameterViewModel ParameterViewModel { get; set; }

        public GenericCommand RunTaskCommand { get; set; }

        public TaskSelectorViewModel()
        {
            InitializeTasker();
            this.CommandList = _tasker.GetTaskCommands().ToList();
            this.Parameters = new ObservableCollection<ParameterItemViewModel>();
            this.ParameterViewModel = new AddParameterViewModel();
            this.ParameterViewModel.Save += ParameterViewModel_Save;
            this.RunTaskCommand = new GenericCommand((Action)RunSelectedTask);
        }

        public void RunSelectedTask()
        {
            var computedTask = _tasker.FindTask(SelectedTask);
            MessageBox.Show(computedTask.Command + " : " + computedTask.Data.Positions.Count());
        }

        private void InitializeTasker()
        {
            _tasker = Tasker.Instance;
            _bootstrapper = new TaskBootstrapper();
            var tasks = _bootstrapper.GetTasks(_options);
            foreach(var task in tasks)
            {
                _tasker.RegisterTask(task);
            }
            _tasker.Initialize();
        }

        private void ParameterViewModel_Save(object sender, System.EventArgs e)
        {
            this.Parameters.Add(new ParameterItemViewModel(this.ParameterViewModel.Parameter));
            this.ParameterViewModel.Reset();
        }
    }
}
