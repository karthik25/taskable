using System;
using System.Collections.ObjectModel;

namespace TaskableApp.ViewModels
{
    public class SettingsTabViewModel : BindableBase
    {
        public TaskSelectorViewModel TaskSelectorModel;

        public ObservableCollection<string> TaskDefitionPaths { get; set; }
        public ObservableCollection<string> AdditionalReferences { get; set; }

        public FileOrFolderSelectionViewModel FileSelectionViewModel { get; set; }
        public FileOrFolderSelectionViewModel FolderSelectionViewModel { get; set; }

        public event EventHandler TasksAdded;
        public event EventHandler ReferencesAdded;

        public SettingsTabViewModel(TaskSelectorViewModel model)
        {
            this.TaskSelectorModel = model;

            this.TaskDefitionPaths = new ObservableCollection<string>(_options.TaskDefinitionPaths);
            this.AdditionalReferences = new ObservableCollection<string>(_options.AdditionalReferences);

            this.FileSelectionViewModel = new FileOrFolderSelectionViewModel(SelectionType.File);
            this.FileSelectionViewModel.Save += FileSelectionViewModel_Save;
            this.FolderSelectionViewModel = new FileOrFolderSelectionViewModel(SelectionType.Folder);
            this.FolderSelectionViewModel.Save += FolderSelectionViewModel_Save;
        }

        private void FolderSelectionViewModel_Save(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FolderSelectionViewModel.FileOrFolder))
                return;

            this.TaskDefitionPaths.Add(this.FolderSelectionViewModel.FileOrFolder);

            OnTasksAdded();
        }

        private void FileSelectionViewModel_Save(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FileSelectionViewModel.FileOrFolder))
                return;

            this.AdditionalReferences.Add(this.FileSelectionViewModel.FileOrFolder);
        }

        private void OnTasksAdded()
        {
            if (TasksAdded != null)
            {
                var handler = TasksAdded;
                handler(this, new EventArgs());
            }
        }
    }
}
