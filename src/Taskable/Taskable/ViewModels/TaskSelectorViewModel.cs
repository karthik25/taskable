using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private TaskResult _taskResult;

        private ObservableCollection<TaskItem> _commandList;
        public ObservableCollection<TaskItem> CommandList
        {
            get { return _commandList; }
            set { SetProperty(ref _commandList, value); }
        }

        public string SelectedTask { get; set; }

        public ObservableCollection<ParameterItemViewModel> Parameters
        {
            get; set;
        }

        private ObservableCollection<Error> _errors;
        public ObservableCollection<Error> Errors
        {
            get { return _errors; }
            set { SetProperty(ref _errors, value); }
        }

        public AddParameterViewModel ParameterViewModel { get; set; }

        public GenericCommand RemoveParameter { get; set; }
        public ParameterItemViewModel SelectedItem { get; set; }
        public GenericCommand RunTaskCommand { get; set; }

        public ObservableCollection<string> OutputEntries
        {
            get; set;
        }

        public SettingsTabViewModel SettingsTabViewModel { get; set; }

        public TaskSelectorViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _tasker = Tasker.Instance;
            _bootstrapper = new TaskBootstrapper();
            InitializeTasker();
            this.Errors = new ObservableCollection<Error>(_taskResult.Errors.Select(e => new Error(e)));
            this.Parameters = new ObservableCollection<ParameterItemViewModel>();
            this.OutputEntries = new ObservableCollection<string>();
            this.ParameterViewModel = new AddParameterViewModel();
            this.ParameterViewModel.Save += ParameterViewModel_Save;
            this.RunTaskCommand = new GenericCommand((Action)RunSelectedTask);
            this.RemoveParameter = new GenericCommand((Action)RemoveParam);
            this.SettingsTabViewModel = new SettingsTabViewModel(this);
            this.SettingsTabViewModel.TasksAdded += SettingsTabViewModel_TasksAdded;
            this.SettingsTabViewModel.ReferencesAdded += SettingsTabViewModel_ReferencesAdded;
        }

        private void SettingsTabViewModel_TasksAdded(object sender, EventArgs e)
        {
            var options = new Options
            {
                TaskDefinitionPaths = SettingsTabViewModel.TaskDefitionPaths.ToList(),
                AdditionalReferences = SettingsTabViewModel.AdditionalReferences.ToList()
            };
            this.UpdateUserSpecificOptions(options);

            this.OutputEntries.Add("Attempting to regenerate the tasks...");
            _tasker.ReleaseTasks();
            InitializeTasker();
            this.OutputEntries.Add("Regenerated & registered the tasks discovered.");
        }

        private void SettingsTabViewModel_ReferencesAdded(object sender, EventArgs e)
        {

        }

        public void TaskSaved()
        {
            this.OutputEntries.Add("Attempting to regenerate the tasks...");
            _tasker.ReleaseTasks();
            InitializeTasker();
            this.OutputEntries.Add("Regenerated & registered the tasks discovered.");
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
            _taskResult = _bootstrapper.GetTasks(_options);
            foreach (var task in _taskResult.Tasks)
            {
                _tasker.RegisterTask(task);
            }
            _tasker.Initialize();
            this.CommandList = new ObservableCollection<TaskItem>(_tasker.GetTaskCommands().Select(t => new TaskItem { Name = t }));
            this.Errors = new ObservableCollection<Error>(_taskResult.Errors.Select(e => new Error(e)));
        }

        private void ParameterViewModel_Save(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ParameterViewModel.Parameter))
                return;

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
