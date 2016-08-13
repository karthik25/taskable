using TaskableApp.ViewModels;

namespace TaskableApp
{
    public class MainWindowViewModel : BindableBase
    {
        public TaskSelectorViewModel TaskSelectorViewModel { get; set; }
        public CodeCollectionViewModel CodeCollectionViewModel { get; set; }

        public MainWindowViewModel()
        {
            this.TaskSelectorViewModel = new TaskSelectorViewModel(this);
            this.CodeCollectionViewModel = new CodeCollectionViewModel(this);
        }
    }
}
