using System;

namespace TaskableApp.ViewModels
{
    public class FileOrFolderSelectionViewModel : BindableBase
    {
        public string FileOrFolder { get; set; }
        public SelectionType SelectionType { get; set; }

        public bool IsFolder
        {
            get { return SelectionType == SelectionType.File; }
            set { }
        }

        public event EventHandler Save;

        public GenericCommand<object> SaveCommand { get; set; }

        public FileOrFolderSelectionViewModel(SelectionType type)
        {
            this.SelectionType = type;
            this.SaveCommand = new GenericCommand<object>(x => this.Save(this, new EventArgs()));
        }

        public void Reset()
        {
            this.FileOrFolder = string.Empty;
        }
    }

    public enum SelectionType
    {
        File,
        Folder
    }
}
