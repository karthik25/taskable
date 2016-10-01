using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskableApp.Models;
using TaskableCore;
using TaskableRoslynCore;
using TaskableScriptCs.Contracts;

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

        public event EventHandler OptionsSaved;

        public TaskSelectorViewModel(MainWindowViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _tasker = Tasker.Instance;
            _bootstrapper = new TaskBootstrapper();
            this.Parameters = new ObservableCollection<ParameterItemViewModel>();
            this.OutputEntries = new ObservableCollection<string>();
            this.ParameterViewModel = new AddParameterViewModel();
            this.ParameterViewModel.Save += ParameterViewModel_Save;
            this.RunTaskCommand = new GenericCommand((Action)RunSelectedTask);
            this.RemoveParameter = new GenericCommand((Action)RemoveParam);
            this.SettingsTabViewModel = new SettingsTabViewModel(this);
            this.SettingsTabViewModel.TasksAddedOrRemoved += SettingsTabViewModel_TasksAdded;
            this.SettingsTabViewModel.ReferencesAddedOrRemoved += SettingsTabViewModel_ReferencesAdded;
            TaskProgress.MessageReceived += TaskProgress_MessageReceived;
        }

        private async void TaskProgress_MessageReceived(object sender, ProgressEventArgs e)
        {
            await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var displayedMessage = e.Type == ProgressType.Log ? e.Message : string.Format("(error) {0}", e.Message);
                this.OutputEntries.Insert(0, displayedMessage);
            }));
        }

        private async void SettingsTabViewModel_TasksAdded(object sender, EventArgs e)
        {
            await SaveOptionsAndReInitTasks();
        }

        private async void SettingsTabViewModel_ReferencesAdded(object sender, EventArgs e)
        {
            await SaveOptionsAndReInitTasks();
        }

        private async Task SaveOptionsAndReInitTasks()
        {
            var options = new Options
            {
                TaskDefinitionPaths = SettingsTabViewModel.TaskDefitionPaths.ToList(),
                AdditionalReferences = SettingsTabViewModel.AdditionalReferences.ToList()
            };
            this.UpdateUserSpecificOptions(options);

            this.OutputEntries.Insert(0, "Attempting to regenerate the tasks...");
            await ReinitializeTasks();

            OnOptionsSaved();
        }

        public async void TaskSaved()
        {
            this.OutputEntries.Insert(0, "Attempting to regenerate the tasks...");
            await ReinitializeTasks();            
        }

        public void OnOptionsSaved()
        {
            OptionsSaved?.Invoke(this, new EventArgs());
        }

        private async void RunSelectedTask()
        {
            await Task.Factory.StartNew(new Action(async () =>
            {
                if (!string.IsNullOrEmpty(SelectedTask))
                {
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this._mainViewModel.ShowLoadingPanel("Running the selected task...");
                    }));

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
                        await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.OutputEntries.Insert(0, "Running: " + finalCommand);
                        }));
                        var runStatus = _tasker.InvokeTask(finalCommand);
                        await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.OutputEntries.Insert(0, "Completed running the task (status): " + runStatus);
                            this._mainViewModel.HideLoadingPanel();
                        }));
                    }
                }
            }));
        }

        public async Task RefreshTaskableInstances()
        {
            await ReinitializeTasks();
        }

        private async Task ReinitializeTasks()
        {
            await Task.Factory.StartNew(new Action(async () =>
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => 
                {
                    _mainViewModel.ShowLoadingPanel();
                }));
                _tasker.ReleaseTasks();
                var bootstrapper = new TaskBootstrapper();
                var taskResult = bootstrapper.GetTasks(_options);
                foreach (var task in taskResult.Tasks)
                {
                    _tasker.RegisterTask(task);
                }
                _tasker.Initialize();
                await Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.CommandList = new ObservableCollection<TaskItem>(_tasker.GetTaskCommands().Select(t => new TaskItem { Name = t }));
                    this.Errors = new ObservableCollection<Error>(taskResult.Errors.Select(e => new Error(e)));
                    this.OutputEntries.Insert(0, "Regenerated & registered the tasks discovered.");
                    _mainViewModel.HideLoadingPanel();
                }));
            }));
        }

        private void ParameterViewModel_Save(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ParameterViewModel.Parameter))
                return;

            this.Parameters.Add(new ParameterItemViewModel(this.ParameterViewModel.Parameter));
            this.OutputEntries.Insert(0, "Parameter added: " + this.ParameterViewModel.Parameter);
            this.ParameterViewModel.Reset();
        }

        private void RemoveParam()
        {
            if (SelectedItem != null)
            {
                this.OutputEntries.Insert(0, "Parameter removed: " + SelectedItem.ParameterValue);
                this.Parameters.Remove(SelectedItem);
            }
        }
    }
}
