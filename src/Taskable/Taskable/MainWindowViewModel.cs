using TaskableApp.ViewModels;

namespace TaskableApp
{
    public class MainWindowViewModel : BindableBase
    {
        public TaskSelectorViewModel TaskSelectorViewModel { get; set; }
        public CodeCollectionViewModel CodeCollectionViewModel { get; set; }

        private bool _panelLoading;
        public bool PanelLoading
        {
            get { return _panelLoading; }
            set { SetProperty(ref _panelLoading, value); }
        }

        public void ShowLoadingPanel()
        {
            this.PanelLoading = true;
        }

        public void HideLoadingPanel()
        {
            this.PanelLoading = false;
        }

        public MainWindowViewModel()
        {
            this.TaskSelectorViewModel = new TaskSelectorViewModel(this);
            this.CodeCollectionViewModel = new CodeCollectionViewModel(this);
        }
    }
}
