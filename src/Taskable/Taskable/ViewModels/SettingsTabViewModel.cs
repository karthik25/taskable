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

        public event EventHandler TasksAddedOrRemoved;
        public event EventHandler ReferencesAddedOrRemoved;

        private string _selectedPath;
        public string SelectedPath
        {
            get { return _selectedPath; }
            set { SetProperty(ref _selectedPath, value); }
        }

        private string _selectedReference;
        public string SelectedReference
        {
            get { return _selectedReference; }
            set { SetProperty(ref _selectedReference, value); }
        }

        public GenericCommand RemovePathCommand { get; set; }
        public GenericCommand RemoveReferenceCommand { get; set; }

        public SettingsTabViewModel(TaskSelectorViewModel model)
        {
            this.TaskSelectorModel = model;

            this.TaskDefitionPaths = new ObservableCollection<string>(_options.TaskDefinitionPaths);
            this.AdditionalReferences = new ObservableCollection<string>(_options.AdditionalReferences);

            this.FileSelectionViewModel = new FileOrFolderSelectionViewModel(SelectionType.File);
            this.FileSelectionViewModel.Save += FileSelectionViewModel_Save;
            this.FolderSelectionViewModel = new FileOrFolderSelectionViewModel(SelectionType.Folder);
            this.FolderSelectionViewModel.Save += FolderSelectionViewModel_Save;

            this.RemovePathCommand = new GenericCommand((Action)RemovePath);
            this.RemoveReferenceCommand = new GenericCommand((Action)RemoveReference);
        }

        private void FolderSelectionViewModel_Save(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FolderSelectionViewModel.FileOrFolder))
                return;

            this.TaskDefitionPaths.Add(this.FolderSelectionViewModel.FileOrFolder);

            OnTasksAddedOrRemoved();
        }

        private void FileSelectionViewModel_Save(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FileSelectionViewModel.FileOrFolder))
                return;

            this.AdditionalReferences.Add(this.FileSelectionViewModel.FileOrFolder);

            OnReferencesAddedOrRemoved();
        }

        private void RemovePath()
        {
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                this.TaskDefitionPaths.Remove(SelectedPath);
                OnTasksAddedOrRemoved();
            }
        }

        private void RemoveReference()
        {
            if (!string.IsNullOrEmpty(SelectedReference))
            {
                this.AdditionalReferences.Remove(SelectedReference);
                OnReferencesAddedOrRemoved();
            }
        }

        private void OnTasksAddedOrRemoved()
        {
            if (TasksAddedOrRemoved != null)
            {
                var handler = TasksAddedOrRemoved;
                handler(this, new EventArgs());
            }
        }

        private void OnReferencesAddedOrRemoved()
        {
            if (ReferencesAddedOrRemoved != null)
            {
                var handler = ReferencesAddedOrRemoved;
                handler(this, new EventArgs());
            }
        }
    }
}
