using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskableApp.Models;
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

        public ObservableCollection<ParameterItemViewModel> Parameters
        {
            get;set;
        }

        public ObservableCollection<Error> Errors
        {
            get;set;
        }

        public AddParameterViewModel ParameterViewModel { get; set; }

        public TaskSelectorViewModel()
        {
            _tasker = Tasker.Instance;
            _bootstrapper = new TaskBootstrapper();
            var tasks = _bootstrapper.GetTasks(_options).Select(t => new ComputedTask(t));
            this.CommandList = tasks.Select(t => t.Command).ToList();
            this.Parameters = new ObservableCollection<ParameterItemViewModel>();
            this.ParameterViewModel = new AddParameterViewModel();
            this.ParameterViewModel.Save += ParameterViewModel_Save;
        }

        private void ParameterViewModel_Save(object sender, System.EventArgs e)
        {
            this.Parameters.Add(new ParameterItemViewModel { ParameterValue = this.ParameterViewModel.Parameter });
            this.ParameterViewModel.Reset();
        }
    }
}
