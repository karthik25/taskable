using TaskableApp.ViewModels;

namespace TaskableApp
{
    public class MainWindowViewModel : BindableBase
    {
        public TaskSelectorViewModel TaskSelectorViewModel { get; set; }
        public CodeCollectionViewModel CodeCollectionViewModel { get; set; }

        private string _currentActivity;
        public string CurrentActivity
        {
            get { return _currentActivity; }
            set { SetProperty(ref _currentActivity, value); }
        }

        private bool _panelLoading;
        public bool PanelLoading
        {
            get { return _panelLoading; }
            set { SetProperty(ref _panelLoading, value); }
        }

        public void ShowLoadingPanel(string currentActivity = "")
        {
            if (!string.IsNullOrEmpty(currentActivity))
                this.CurrentActivity = currentActivity;
            this.PanelLoading = true;
        }

        public void HideLoadingPanel()
        {
            this.PanelLoading = false;
        }

        public MainWindowViewModel()
        {
            this.CurrentActivity = "Generating tasks...";
            this.TaskSelectorViewModel = new TaskSelectorViewModel(this);
            this.CodeCollectionViewModel = new CodeCollectionViewModel(this);
        }
    }
}
