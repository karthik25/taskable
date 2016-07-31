using System.Collections.Generic;
using TaskableCore;

namespace TaskableApp.ViewModels
{
    public class TaskSelectorViewModel : BindableBase
    {
        private Tasker _tasker;

        public List<string> CommandList
        {
            get;set;
        }

        public TaskSelectorViewModel()
        {
            _tasker = Tasker.Instance;
            this.CommandList = new List<string>
            {
                "echo",
                "git-download",
                "git-download-s"
            };
        }
    }
}
