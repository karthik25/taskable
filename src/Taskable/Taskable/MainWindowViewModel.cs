using TaskableApp.ViewModels;

namespace TaskableApp
{
    public class MainWindowViewModel : BindableBase
    {
        public TaskSelectorViewModel TaskSelectorViewModel { get; set; }

        public MainWindowViewModel()
        {
            this.TaskSelectorViewModel = new TaskSelectorViewModel();   
        }
    }
}
