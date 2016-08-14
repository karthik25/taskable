using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TaskableApp.Models;
using TaskableCore;
using TaskableRoslynCore;

namespace TaskableApp.ViewModels
{
    public class TaskSelectorViewModel : BindableBase
    {
        private MainWindowViewModel _mainViewModel;
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

        public GenericCommand RemoveParameter { get; set; }
        public ParameterItemViewModel SelectedItem { get; set; }
        public GenericCommand RunTaskCommand { get; set; }

        public ObservableCollection<string> OutputEntries
        {
            get;set;
        }
        
        public TaskSelectorViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _tasker = Tasker.Instance;
            InitializeTasker();
            this.CommandList = _tasker.GetTaskCommands().ToList();
            this.Parameters = new ObservableCollection<ParameterItemViewModel>();
            this.OutputEntries = new ObservableCollection<string>();
            this.ParameterViewModel = new AddParameterViewModel();
            this.ParameterViewModel.Save += ParameterViewModel_Save;
            this.RunTaskCommand = new GenericCommand((Action)RunSelectedTask);
            this.RemoveParameter = new GenericCommand((Action)RemoveParam);
        }

        public void TaskSaved()
        {
            this.OutputEntries.Clear();
            this.OutputEntries.Add("Regenerating the tasks");
        } 

        private void RunSelectedTask()
        {
            if (!string.IsNullOrEmpty(SelectedTask))
            {
                var computedTask = _tasker.FindTask(SelectedTask);
                if (computedTask.Data.Positions.Count() == Parameters.Count)
                {
                    var parameterIndex = 0;
                    var cmdSplit = computedTask.Pattern.Split(new[] { ' ' }).ToList();
                    for (int i = 0; i < cmdSplit.Count; i++)
                    {
                        if (cmdSplit[i] == "{}")
                        {
                            cmdSplit[i] = Parameters[parameterIndex].ParameterValue;
                            parameterIndex++;
                        }
                    }
                    var finalCommand = string.Join(" ", cmdSplit);
                    OutputEntries.Add("Running: " + finalCommand);
                    var runStatus = _tasker.InvokeTask(finalCommand);
                    OutputEntries.Add("Completed running the task (status): " + runStatus);
                }
            }
        }

        private void InitializeTasker()
        {
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
            this.OutputEntries.Add("Parameter added: " + this.ParameterViewModel.Parameter);
            this.ParameterViewModel.Reset();
        }

        private void RemoveParam()
        {
            if (SelectedItem != null)
            {
                this.OutputEntries.Add("Parameter removed: " + SelectedItem.ParameterValue);
                this.Parameters.Remove(SelectedItem);
            }
        }
    }
}
